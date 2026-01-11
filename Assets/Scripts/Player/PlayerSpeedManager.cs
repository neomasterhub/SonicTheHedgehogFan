using System;
using UnityEngine;

public class PlayerSpeedManager
{
  private readonly InputInfo _inputInfo;
  private float _groundSpeed;

  public PlayerSpeedManager(InputInfo inputInfo)
  {
    _inputInfo = inputInfo;
  }

  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }

  public void SetSpeed(PlayerState playerState, PlayerSpeedInput input)
  {
    if (playerState.HasFlag(PlayerState.Airborne))
    {
      SetSpeed_Airborne(input);
    }
    else if (playerState.HasFlag(PlayerState.Grounded))
    {
      SetSpeed_Grounded(input);
    }
  }

  private void SetSpeed_Airborne(PlayerSpeedInput input)
  {
    SetSpeed_Airborne_Gravity(input);
    SetSpeed_Airborne_PreventGroundOvershoot(input);
    SetSpeed_Airborne_Horizontal(input);
  }

  private void SetSpeed_Airborne_Gravity(PlayerSpeedInput input)
  {
    if (!input.GravityDownEnabled)
    {
      return;
    }

    SpeedY -= SpeedY > 0
      ? input.GravityUpSpeed
      : input.GravityDownSpeed;

    if (SpeedY < -input.MaxFallSpeed)
    {
      SpeedY = -input.MaxFallSpeed;
    }
  }

  private void SetSpeed_Airborne_PreventGroundOvershoot(PlayerSpeedInput input)
  {
    if (SpeedY > 0)
    {
      return;
    }

    // Keeps surface normal aligned with slope.
    var yOffset = input.GroundSensorLength / 2;

    SpeedY = -Mathf.Min(Mathf.Abs(SpeedY), input.DistanceToGround - yOffset);
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

    SpeedX = _groundSpeed * MathF.Cos(input.GroundAngleRad);
    SpeedY = _groundSpeed * MathF.Sin(input.GroundAngleRad);
  }

  private void SetSpeed_Grounded_Forward(PlayerSpeedInput input)
  {
    if (_groundSpeed < 0)
    {
      _groundSpeed += input.DecelerationSpeed;

      if (_groundSpeed >= 0)
      {
        _groundSpeed = input.DecelerationSpeed;
      }
    }
    else if (_groundSpeed < input.TopSpeed)
    {
      _groundSpeed += input.AccelerationSpeed;

      if (_groundSpeed >= input.TopSpeed)
      {
        _groundSpeed = input.TopSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Backward(PlayerSpeedInput input)
  {
    if (_groundSpeed > 0)
    {
      _groundSpeed -= input.DecelerationSpeed;

      if (_groundSpeed <= 0)
      {
        _groundSpeed = -input.DecelerationSpeed;
      }
    }
    else if (_groundSpeed > -input.TopSpeed)
    {
      _groundSpeed -= input.AccelerationSpeed;

      if (_groundSpeed <= -input.TopSpeed)
      {
        _groundSpeed = -input.TopSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Friction(PlayerSpeedInput input)
  {
    if (Mathf.Abs(_groundSpeed) < input.GroundSpeedDeadZone)
    {
      _groundSpeed = 0;
      return;
    }

    _groundSpeed -= input.FrictionSpeed * Mathf.Sign(_groundSpeed);
  }
}
