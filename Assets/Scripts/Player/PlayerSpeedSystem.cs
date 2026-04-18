using System;
using UnityEngine;
using static SharedConsts.Input;

public class PlayerSpeedSystem
{
  private const int _speedRoundingDigits = 3;
  private const int _zeroGroundSpeedProgressMax = 5;

  private readonly ConditionalValueProvider<float> _slopeFactorSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _airToGroundSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerSpeedConfig _config;

  private float _groundAngleCos;
  private float _groundAngleSin;
  private PlayerSpeedContext _context;

  public PlayerSpeedSystem(
    PlayerInputSystem inputSystem,
    PlayerSpeedConfig config,
    ConditionalValueProvider<float> slopeFactorSpeedProvider,
    ConditionalValueProvider<Vector2> airToGroundSpeedProvider,
    ConditionalValueProvider<Vector2> groundToAirSpeedProvider,
    ConditionalValueProvider<GravitySpeed> gravitySpeedProvider)
  {
    _inputSystem = inputSystem;
    _config = config;
    _slopeFactorSpeedProvider = slopeFactorSpeedProvider;
    _airToGroundSpeedProvider = airToGroundSpeedProvider;
    _groundToAirSpeedProvider = groundToAirSpeedProvider;
    _gravitySpeedProvider = gravitySpeedProvider;
  }

  public bool IsSkidding { get; private set; }
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }
  public float GroundSpeed { get; private set; }
  public float SlopeFactorSpeed { get; private set; }
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

  public void SetSpeed(PlayerSpeedContext context)
  {
    _context = context;

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
    if (Mathf.Abs(_inputSystem.X) < InputDeadZone)
    {
      return;
    }

    SpeedX += _inputSystem.X * _config.AirAccelerationSpeed;

    if (Mathf.Abs(SpeedX) > _config.AirTopSpeed)
    {
      SpeedX = _config.AirTopSpeed * Mathf.Sign(SpeedX);
    }
  }

  private void SetSpeed_Grounded()
  {
    _groundAngleCos = MathF.Cos(_context.GroundAngleRad.Value);
    _groundAngleSin = MathF.Sin(_context.GroundAngleRad.Value);

    SetSpeed_Grounded_FromAirborne();
    SetSpeed_Grounded_Slope();

    if (_inputSystem.X > InputDeadZone)
    {
      SetSpeed_Grounded_Forward();
    }
    else if (_inputSystem.X < -InputDeadZone)
    {
      SetSpeed_Grounded_Backward();
    }
    else
    {
      SetSpeed_Grounded_Friction();
    }

    SpeedX = GroundSpeed * _groundAngleCos;
    SpeedY = GroundSpeed * _groundAngleSin;

    if (Mathf.Abs(GroundSpeed) < _config.FrictionSpeed)
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

  private void SetSpeed_Grounded_Slope()
  {
    SlopeFactorSpeed = _slopeFactorSpeedProvider.FirstTriggeredOrDefault();
    GroundSpeed -= SlopeFactorSpeed;
  }

  private void SetSpeed_Grounded_Forward()
  {
    if (GroundSpeed < 0)
    {
      if (GroundSpeed < -_config.MaxSkiddingSpeed)
      {
        IsSkidding = true;
      }

      GroundSpeed += _config.DecelerationSpeed;

      if (GroundSpeed >= 0)
      {
        IsSkidding = false;
        GroundSpeed = _config.DecelerationSpeed;
      }
    }
    else if (GroundSpeed < _config.TopSpeed)
    {
      GroundSpeed += _config.AccelerationSpeed;

      if (GroundSpeed >= _config.TopSpeed)
      {
        GroundSpeed = _config.TopSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Backward()
  {
    if (GroundSpeed > 0)
    {
      if (GroundSpeed > _config.MaxSkiddingSpeed)
      {
        IsSkidding = true;
      }

      GroundSpeed -= _config.DecelerationSpeed;

      if (GroundSpeed <= 0)
      {
        IsSkidding = false;
        GroundSpeed = -_config.DecelerationSpeed;
      }
    }
    else if (GroundSpeed > -_config.TopSpeed)
    {
      GroundSpeed -= _config.AccelerationSpeed;

      if (GroundSpeed <= -_config.TopSpeed)
      {
        GroundSpeed = -_config.TopSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Friction()
  {
    if (Mathf.Abs(GroundSpeed) < _config.FrictionSpeed)
    {
      GroundSpeed = 0;
      IsSkidding = false;
      return;
    }

    GroundSpeed -= _config.FrictionSpeed * Mathf.Sign(GroundSpeed);
  }
}
