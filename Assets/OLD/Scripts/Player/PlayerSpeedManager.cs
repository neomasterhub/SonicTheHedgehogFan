using System;
using UnityEngine;

public class PlayerSpeedManager
{
  private const int _speedDigits = 3;

  private readonly InputInfo _inputInfo;
  private readonly SpeedProvider<GravitySpeed> _gravitySpeedProvider = new();
  private readonly SpeedProvider<float> _slopeFactorSpeedProvider;
  private readonly SpeedProvider<Vector2> _groundToAirSpeedProvider;

  private float _groundAngleCos;
  private float _groundAngleSin;

  public PlayerSpeedManager(
    InputInfo inputInfo,
    SpeedProvider<GravitySpeed> gravitySpeedProvider,
    SpeedProvider<float> slopeFactorSpeedProvider,
    SpeedProvider<Vector2> groundToAirSpeedProvider)
  {
    _inputInfo = inputInfo;
    _gravitySpeedProvider = gravitySpeedProvider;
    _slopeFactorSpeedProvider = slopeFactorSpeedProvider;
    _groundToAirSpeedProvider = groundToAirSpeedProvider;
  }

  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }
  public float SlopeFactorSpeed { get; private set; }
  public float GroundSpeed { get; private set; }
  public bool IsSkidding { get; private set; }
  public GravitySpeed GravitySpeed { get; private set; }

  public void ResetSpeeds()
  {
    SpeedX = 0;
    SpeedY = 0;
    GroundSpeed = 0;
    _groundAngleCos = 0;
    _groundAngleSin = 0;
  }

  public void SetSpeed(PlayerSpeedInput input)
  {
    if (input.PlayerState.HasFlag(SonicState.Airborne))
    {
      SetSpeed_Airborne(input);
    }
    else if (input.PlayerState.HasFlag(SonicState.Grounded))
    {
      SetSpeed_Grounded(input);
    }

    RoundSpeeds();
  }

  private void RoundSpeeds()
  {
    SpeedX = SpeedX.Round(_speedDigits);
    SpeedY = SpeedY.Round(_speedDigits);
    GroundSpeed = GroundSpeed.Round(_speedDigits);
  }

  private void SetSpeed_Airborne(PlayerSpeedInput input)
  {
    IsSkidding = false;
    SetSpeed_Airborne_FromGrounded(input);
    SetSpeed_Airborne_Gravity(input);
    SetSpeed_Airborne_PreventGroundOvershoot(input);
    SetSpeed_Airborne_Horizontal(input);
  }

  private void SetSpeed_Airborne_FromGrounded(PlayerSpeedInput input)
  {
    if (!input.PrevPlayerState.HasFlag(SonicState.Grounded))
    {
      return;
    }

    var speed = _groundToAirSpeedProvider.FirstTriggeredOrDefault();
    SpeedX = speed.x;
    SpeedY = speed.y;
  }

  private void SetSpeed_Airborne_Gravity(PlayerSpeedInput input)
  {
    GravitySpeed = _gravitySpeedProvider.FirstTriggeredOrDefault();

    SpeedY -= SpeedY > 0 ? GravitySpeed.Up : GravitySpeed.Down;

    if (SpeedY < -input.MaxFallSpeed)
    {
      SpeedY = -input.MaxFallSpeed;
    }
  }

  private void SetSpeed_Airborne_PreventGroundOvershoot(PlayerSpeedInput input)
  {
    if (SpeedY < 0)
    {
      SpeedY = -Mathf.Min(Mathf.Abs(SpeedY), input.DistanceToGround);
    }
  }

  private void SetSpeed_Airborne_Horizontal(PlayerSpeedInput input)
  {
    if (Mathf.Abs(_inputInfo.X) < input.InputDeadZone)
    {
      return;
    }

    SpeedX += _inputInfo.X * input.AirAccelerationSpeed;

    if (Mathf.Abs(SpeedX) > input.AirTopSpeed)
    {
      SpeedX = input.AirTopSpeed * Mathf.Sign(SpeedX);
    }
  }

  private void SetSpeed_Grounded(PlayerSpeedInput input)
  {
    _groundAngleCos = MathF.Cos(input.GroundAngleRad);
    _groundAngleSin = MathF.Sin(input.GroundAngleRad);

    SetSpeed_Grounded_FromAirborne(input);
    SetSpeed_Grounded_Slope(input);

    if (_inputInfo.X > 0)
    {
      SetSpeed_Grounded_Forward(input);
    }
    else if (_inputInfo.X < 0)
    {
      SetSpeed_Grounded_Backward(input);
    }
    else
    {
      SetSpeed_Grounded_Friction(input);
    }

    SpeedX = GroundSpeed * _groundAngleCos;
    SpeedY = GroundSpeed * _groundAngleSin;
  }

  private void SetSpeed_Grounded_FromAirborne(PlayerSpeedInput input)
  {
    if (input.PrevPlayerState.HasFlag(SonicState.Airborne))
    {
      GroundSpeed = Mathf.Clamp(
        (SpeedX * _groundAngleCos) + (SpeedY * _groundAngleSin),
        -input.TopSpeed,
        input.TopSpeed);
    }
  }

  private void SetSpeed_Grounded_Slope(PlayerSpeedInput input)
  {
    SlopeFactorSpeed = _slopeFactorSpeedProvider.FirstTriggeredOrDefault();
    GroundSpeed -= SlopeFactorSpeed;
  }

  private void SetSpeed_Grounded_Forward(PlayerSpeedInput input)
  {
    if (GroundSpeed < 0)
    {
      if (GroundSpeed < -input.SkiddingSpeedDeadZone)
      {
        IsSkidding = true;
      }

      GroundSpeed += input.DecelerationSpeed;

      if (GroundSpeed >= 0)
      {
        IsSkidding = false;
        GroundSpeed = input.DecelerationSpeed;
      }
    }
    else if (GroundSpeed < input.TopSpeed)
    {
      GroundSpeed += input.AccelerationSpeed;

      if (GroundSpeed >= input.TopSpeed)
      {
        GroundSpeed = input.TopSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Backward(PlayerSpeedInput input)
  {
    if (GroundSpeed > 0)
    {
      if (GroundSpeed > input.SkiddingSpeedDeadZone)
      {
        IsSkidding = true;
      }

      GroundSpeed -= input.DecelerationSpeed;

      if (GroundSpeed <= 0)
      {
        IsSkidding = false;
        GroundSpeed = -input.DecelerationSpeed;
      }
    }
    else if (GroundSpeed > -input.TopSpeed)
    {
      GroundSpeed -= input.AccelerationSpeed;

      if (GroundSpeed <= -input.TopSpeed)
      {
        GroundSpeed = -input.TopSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Friction(PlayerSpeedInput input)
  {
    if (Mathf.Abs(GroundSpeed) < input.FrictionSpeed)
    {
      GroundSpeed = 0;
      IsSkidding = false;
      return;
    }

    GroundSpeed -= input.FrictionSpeed * Mathf.Sign(GroundSpeed);
  }
}
