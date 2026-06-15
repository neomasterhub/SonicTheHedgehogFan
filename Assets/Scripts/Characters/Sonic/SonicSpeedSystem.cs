using System;
using UnityEngine;
using static SharedConsts.Physics;
using static SonicConsts.Physics;

public class SonicSpeedSystem : SpeedSystemBase
{
  private const int _zeroGroundSpeedProgressMax = 5;

  private readonly SonicConfigs _configs;
  private readonly PlayerInputSystem _inputSystem;
  private readonly ConditionalValueProvider<float> _slopeSpeedProvider;
  private readonly ConditionalValueProvider<float> _gravitySpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _airToGroundSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _reboundSpeedProvider;

  private bool _friction;
  private bool _prevIsPushing;
  private float _accSpeed;
  private float _decSpeed;
  private float _frictionSpeed;
  private float _reverseStartSpeed;
  private float _groundAngleCos;
  private float _groundAngleSin;
  private SonicSpeedContext _context;
  private SonicPhysicsModeConfig _config;

  public SonicSpeedSystem(
    SonicConfigs configs,
    PlayerInputSystem inputSystem,
    ConditionalValueProvider<float> slopeSpeedProvider,
    ConditionalValueProvider<float> gravitySpeedProvider,
    ConditionalValueProvider<Vector2> airToGroundSpeedProvider,
    ConditionalValueProvider<Vector2> groundToAirSpeedProvider,
    ConditionalValueProvider<Vector2> reboundSpeedProvider)
  {
    _configs = configs;
    _inputSystem = inputSystem;
    _slopeSpeedProvider = slopeSpeedProvider;
    _gravitySpeedProvider = gravitySpeedProvider;
    _airToGroundSpeedProvider = airToGroundSpeedProvider;
    _groundToAirSpeedProvider = groundToAirSpeedProvider;
    _reboundSpeedProvider = reboundSpeedProvider;
  }

  public bool IsPushing { get; private set; }
  public bool IsSkidding { get; private set; }
  public bool IsStoppedByLeftWall { get; private set; }
  public bool IsStoppedByRightWall { get; private set; }
  public float SlopeSpeed { get; private set; }
  public float GroundSpeed { get; private set; }
  public float GravitySpeed { get; private set; }
  public float MinWallSpeed { get; private set; }
  public float MinCeilingSpeed { get; private set; }
  public int ZeroGroundSpeedProgress { get; private set; }
  public bool IsZeroGroundSpeedProgressReached { get; private set; }

  public override void RoundSpeed()
  {
    base.RoundSpeed();
    GroundSpeed = GroundSpeed.Round(SpeedRoundingDigits);
  }

  public void ResetSpeeds()
  {
    SpeedX = 0;
    SpeedY = 0;
    GroundSpeed = 0;
    _groundAngleCos = 0;
    _groundAngleSin = 0;
  }

  public void SetSpeed(SonicSpeedContext context)
  {
    _context = context;

    SetStateData();

    if (_context.IsHit)
    {
      SetSpeed_HitKnockback();
    }
    else if (_context.IsGrounded)
    {
      SetSpeed_Grounded();
    }
    else
    {
      SetSpeed_Airborne();
    }

    RoundSpeed();
  }

  private void SetStateData()
  {
    _config = _configs.PhysicsModeConfig;
    _reverseStartSpeed = _config.DecelerationSpeed;

    if (_context.IsRolling)
    {
      _accSpeed = 0;
      _decSpeed = _config.RollDecelerationSpeed;

      _friction = true;
      _frictionSpeed = _config.RollFrictionSpeed;

      MinWallSpeed = _config.RollMinWallSpeed;
      MinCeilingSpeed = _config.RollMinCeilingSpeed;
    }
    else
    {
      _accSpeed = _config.AccelerationSpeed;
      _decSpeed = _config.DecelerationSpeed;

      _friction = _inputSystem.X == 0;
      _frictionSpeed = _config.FrictionSpeed;

      MinWallSpeed = _config.MinWallSpeed;
      MinCeilingSpeed = _config.MinCeilingSpeed;
    }
  }

  private void SetSpeed_HitKnockback()
  {
    if (_context.IsDying)
    {
      SpeedX = 0;
      SpeedY = _config.DeathBounceSpeed;

      return;
    }

    SpeedX = _config.HurtSpeedX * _context.HitHorizontalDirection;
    SpeedY = _config.HurtSpeedY;
  }

  private void SetSpeed_Airborne()
  {
    IsPushing = false;
    IsSkidding = false;
    SlopeSpeed = 0;

    SetSpeed_Airborne_FromGrounded();
    SetSpeed_Airborne_Rebound();
    SetSpeed_Airborne_PreventCeilingOvershoot();
    SetSpeed_Airborne_Gravity();
    SetSpeed_Airborne_PreventGroundOvershoot();
    SetSpeed_Airborne_Horizontal();
    SetSpeed_Airborne_PreventWallOvershoot();
  }

  private void SetSpeed_Airborne_FromGrounded()
  {
    if (_context.PrevIsGrounded)
    {
      var speed = _groundToAirSpeedProvider.FirstTriggeredOrDefault();
      SpeedX = speed.x;
      SpeedY = speed.y;
    }
  }

  private void SetSpeed_Airborne_Rebound()
  {
    var speed = _reboundSpeedProvider.FirstTriggeredOrDefault();
    SpeedX = speed.x;
    SpeedY = speed.y;
  }

  private void SetSpeed_Airborne_PreventCeilingOvershoot()
  {
    if (_context.CeilingAngleDeg.HasValue
      && _context.CeilingAngleDeg.Value != 0
      && SpeedY > 0
      && (_context.DistanceToLeftWall.HasValue || _context.DistanceToRightWall.HasValue))
    {
      SpeedY = 0;
    }
  }

  private void SetSpeed_Airborne_Gravity()
  {
    if (_context.IsJumping
      && SpeedY > _config.JumpCutoffSpeed
      && _inputSystem.Released.HasAny(PlayerInput.C))
    {
      SpeedY = _config.JumpCutoffSpeed;
    }

    GravitySpeed = _gravitySpeedProvider.FirstTriggeredOrDefault();

    SpeedY -= GravitySpeed;

    if (SpeedY < -_config.MaxFallSpeed)
    {
      SpeedY = -_config.MaxFallSpeed;
    }
  }

  private void SetSpeed_Airborne_PreventGroundOvershoot()
  {
    if (SpeedY < 0 && _context.DistanceToGround != null)
    {
      SpeedY = Mathf.Max(SpeedY, -_context.DistanceToGround.Value);
    }
  }

  private void SetSpeed_Airborne_Horizontal()
  {
    if (_inputSystem.X == 0)
    {
      return;
    }

    SpeedX += _inputSystem.X * _config.AirAccelerationSpeed;

    if (Mathf.Abs(SpeedX) > _config.AirTopSpeed)
    {
      SpeedX = _config.AirTopSpeed * Mathf.Sign(SpeedX);
    }
  }

  private void SetSpeed_Airborne_PreventWallOvershoot()
  {
    if (IsStoppedByWall(SpeedX))
    {
      SpeedX = 0;
    }
  }

  private void SetSpeed_Grounded()
  {
    _groundAngleCos = MathF.Cos(_context.GroundAngleRad.Value);
    _groundAngleSin = MathF.Sin(_context.GroundAngleRad.Value);

    _prevIsPushing = IsPushing;
    IsPushing = false;

    GravitySpeed = 0;
    SetSpeed_Grounded_FromAirborne();

    if (_context.IsJumping)
    {
      SetSpeed_Grounded_Jump();
      return;
    }

    SetSpeed_Grounded_Slope();

    if (_inputSystem.X > 0)
    {
      SetSpeed_Grounded_Forward();
    }
    else if (_inputSystem.X < 0)
    {
      SetSpeed_Grounded_Backward();
    }

    if (_friction)
    {
      SetSpeed_Grounded_Friction();
    }

    SetSpeed_Grounded_PreventWallOvershoot();
    SetSpeed_Grounded_StopByPushedBlockWall();
    SetSpeed_Grounded_Pushing();

    SpeedX = GroundSpeed * _groundAngleCos;
    SpeedY = GroundSpeed * _groundAngleSin;

    if (Mathf.Abs(GroundSpeed) < _frictionSpeed)
    {
      ZeroGroundSpeedProgress = Mathf.Min(ZeroGroundSpeedProgress + 1, _zeroGroundSpeedProgressMax);
      IsZeroGroundSpeedProgressReached = ZeroGroundSpeedProgress == _zeroGroundSpeedProgressMax;
    }
    else
    {
      ZeroGroundSpeedProgress = 0;
      IsZeroGroundSpeedProgressReached = false;
    }
  }

  private void SetSpeed_Grounded_FromAirborne()
  {
    if (!_context.PrevIsGrounded)
    {
      var speed = _airToGroundSpeedProvider.FirstTriggeredOrDefault();
      SpeedX = speed.x;
      SpeedY = speed.y;

      GroundSpeed = Mathf.Clamp(
        (SpeedX * _groundAngleCos) + (SpeedY * _groundAngleSin),
        -_config.TopSpeed,
        _config.TopSpeed);
    }
  }

  private void SetSpeed_Grounded_Jump()
  {
    SpeedX -= _config.JumpSpeed * _groundAngleSin;
    SpeedY += _config.JumpSpeed * _groundAngleCos;
  }

  private void SetSpeed_Grounded_Slope()
  {
    SlopeSpeed = _slopeSpeedProvider.FirstTriggeredOrDefault();
    GroundSpeed -= SlopeSpeed;
  }

  private void SetSpeed_Grounded_Forward()
  {
    if (GroundSpeed < 0)
    {
      if (!_context.IsRolling && GroundSpeed < -_config.MinSkiddingSpeed)
      {
        IsSkidding = true;
      }

      GroundSpeed += _decSpeed;

      if (GroundSpeed >= 0)
      {
        _friction = false;
        IsSkidding = false;
        GroundSpeed = _reverseStartSpeed;
      }
    }
    else
    {
      IsSkidding = false;

      if (GroundSpeed < _config.TopSpeed)
      {
        GroundSpeed += _accSpeed;

        if (GroundSpeed >= _config.TopSpeed)
        {
          GroundSpeed = _config.TopSpeed;
        }
      }
    }
  }

  private void SetSpeed_Grounded_Backward()
  {
    if (GroundSpeed > 0)
    {
      if (!_context.IsRolling && GroundSpeed > _config.MinSkiddingSpeed)
      {
        IsSkidding = true;
      }

      GroundSpeed -= _decSpeed;

      if (GroundSpeed <= 0)
      {
        _friction = false;
        IsSkidding = false;
        GroundSpeed = -_reverseStartSpeed;
      }
    }
    else
    {
      IsSkidding = false;

      if (GroundSpeed > -_config.TopSpeed)
      {
        GroundSpeed -= _accSpeed;

        if (GroundSpeed <= -_config.TopSpeed)
        {
          GroundSpeed = -_config.TopSpeed;
        }
      }
    }
  }

  private void SetSpeed_Grounded_Friction()
  {
    if (Mathf.Abs(GroundSpeed) < _frictionSpeed)
    {
      GroundSpeed = 0;
      IsSkidding = false;
      return;
    }

    GroundSpeed -= _frictionSpeed * Mathf.Sign(GroundSpeed);
  }

  private void SetSpeed_Grounded_PreventWallOvershoot()
  {
    if (!IsStoppedByWall(GroundSpeed))
    {
      return;
    }

    GroundSpeed = 0;

    if (_context.ContactBlock == null
      || IsPushing)
    {
      return;
    }

    if (_context.DistanceToLeftWall != null)
    {
      var dist = (_context.DistanceToLeftWall.Value - WallClearance).Round(SpeedRoundingDigits);
      if (dist > 0)
      {
        GroundSpeed = -dist;
      }
    }
    else if (_context.DistanceToRightWall != null)
    {
      var dist = (_context.DistanceToRightWall.Value - WallClearance).Round(SpeedRoundingDigits);
      if (dist > 0)
      {
        GroundSpeed = dist;
      }
    }
  }

  private void SetSpeed_Grounded_StopByPushedBlockWall()
  {
    if (_prevIsPushing)
    {
      if (!IsStoppedByLeftWall && _inputSystem.X < 0)
      {
        IsStoppedByLeftWall = true;
      }

      if (!IsStoppedByRightWall && _inputSystem.X > 0)
      {
        IsStoppedByRightWall = true;
      }
    }
  }

  private void SetSpeed_Grounded_Pushing()
  {
    if (_context.ContactBlock == null
      || GroundSpeed != 0)
    {
      return;
    }

    if (IsStoppedByLeftWall && _inputSystem.X < 0)
    {
      IsPushing = true;
      GroundSpeed = -_context.ContactBlock.LeftPushSpeed;

      return;
    }

    if (IsStoppedByRightWall && _inputSystem.X > 0)
    {
      IsPushing = true;
      GroundSpeed = _context.ContactBlock.RightPushSpeed;

      return;
    }
  }

  private bool IsStoppedByWall(float speed)
  {
    IsStoppedByLeftWall = _context.DistanceToLeftWall != null
      && speed <= -_context.DistanceToLeftWall + WallClearance;

    IsStoppedByRightWall = _context.DistanceToRightWall != null
      && speed >= _context.DistanceToRightWall - WallClearance;

    return IsStoppedByLeftWall || IsStoppedByRightWall;
  }
}
