using System;
using UnityEngine;
using static SharedConsts.Physics;

public class SonicSpeedSystem
{
  private const int _speedRoundingDigits = 3;
  private const int _zeroGroundSpeedProgressMax = 5;

  private readonly ConditionalValueProvider<float> _slopeSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _airToGroundSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly PlayerInputSystem _inputSystem;
  private readonly SonicSpeedConfig _config;

  private bool _friction;
  private float _accSpeed;
  private float _decSpeed;
  private float _frictionSpeed;
  private float _reverseStartSpeed;
  private float _groundAngleCos;
  private float _groundAngleSin;
  private SonicSpeedContext _context;

  public SonicSpeedSystem(
    PlayerInputSystem inputSystem,
    SonicSpeedConfig config,
    ConditionalValueProvider<float> slopeSpeedProvider,
    ConditionalValueProvider<Vector2> airToGroundSpeedProvider,
    ConditionalValueProvider<Vector2> groundToAirSpeedProvider,
    ConditionalValueProvider<GravitySpeed> gravitySpeedProvider)
  {
    _inputSystem = inputSystem;
    _config = config;
    _slopeSpeedProvider = slopeSpeedProvider;
    _airToGroundSpeedProvider = airToGroundSpeedProvider;
    _groundToAirSpeedProvider = groundToAirSpeedProvider;
    _gravitySpeedProvider = gravitySpeedProvider;
  }

  public bool IsSkidding { get; private set; }
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }
  public float GroundSpeed { get; private set; }
  public float SlopeSpeed { get; private set; }
  public int ZeroGroundSpeedProgress { get; private set; }
  public bool IsZeroGroundSpeedProgressReached { get; private set; }
  public GravitySpeed GravitySpeed { get; private set; }

  private void RoundSpeeds()
  {
    SpeedX = SpeedX.Round(_speedRoundingDigits);
    SpeedY = SpeedY.Round(_speedRoundingDigits);
    GroundSpeed = GroundSpeed.Round(_speedRoundingDigits);
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

    _reverseStartSpeed = _config.DecelerationSpeed;

    if (_context.IsRolling)
    {
      _accSpeed = 0;
      _decSpeed = _config.RollDecelerationSpeed;

      _friction = true;
      _frictionSpeed = _config.RollFrictionSpeed;
    }
    else
    {
      _accSpeed = _config.AccelerationSpeed;
      _decSpeed = _config.DecelerationSpeed;

      _friction = _inputSystem.X == 0;
      _frictionSpeed = _config.FrictionSpeed;
    }

    if (_context.IsGrounded)
    {
      SetSpeed_Grounded();
    }
    else
    {
      SetSpeed_Airborne();
    }

    RoundSpeeds();
  }

  private void SetSpeed_Airborne()
  {
    IsSkidding = false;
    SetSpeed_Airborne_FromGrounded();
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

  private void SetSpeed_Airborne_Gravity()
  {
    GravitySpeed = _gravitySpeedProvider.FirstTriggeredOrDefault();

    SpeedY -= SpeedY > 0 ? GravitySpeed.Up : GravitySpeed.Down;

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
    if ((_context.DistanceToLeftWall != null
      && SpeedX <= -_context.DistanceToLeftWall + PositionBackwardOffset)
      || (_context.DistanceToRightWall != null
      && SpeedX >= _context.DistanceToRightWall - PositionBackwardOffset))
    {
      SpeedX = 0;
    }
  }

  private void SetSpeed_Grounded()
  {
    _groundAngleCos = MathF.Cos(_context.GroundAngleRad.Value);
    _groundAngleSin = MathF.Sin(_context.GroundAngleRad.Value);

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
    SpeedX -= 0.08f * _groundAngleSin;
    SpeedY += 0.08f * _groundAngleCos;
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
    if ((_context.DistanceToLeftWall != null
      && GroundSpeed <= -_context.DistanceToLeftWall + PositionBackwardOffset)
      || (_context.DistanceToRightWall != null
      && GroundSpeed >= _context.DistanceToRightWall - PositionBackwardOffset))
    {
      GroundSpeed = 0;
    }
  }
}
