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

  public void SetSpeed(
    PlayerState playerState,
    float groundAngleRad,
    float topSpeed,
    float accelerationSpeed,
    float decelerationSpeed,
    float frictionSpeed,
    float groundSpeedDeadZone)
  {
    if (playerState.HasFlag(PlayerState.Airborne))
    {
      SetSpeed_Airborne();
    }
    else if (playerState.HasFlag(PlayerState.Grounded))
    {
      SetSpeed_Grounded(topSpeed, accelerationSpeed, decelerationSpeed, frictionSpeed, groundSpeedDeadZone, groundAngleRad);
    }
  }

  private void SetSpeed_Airborne()
  {
  }

  private void SetSpeed_Grounded(
    float topSpeed,
    float accelerationSpeed,
    float decelerationSpeed,
    float frictionSpeed,
    float groundSpeedDeadZone,
    float groundAngleRad)
  {
    if (_inputInfo.X > 0)
    {
      SetSpeed_Grounded_Forward(topSpeed, accelerationSpeed, decelerationSpeed);
    }
    else if (_inputInfo.X < 0)
    {
      SetSpeed_Grounded_Back(topSpeed, accelerationSpeed, decelerationSpeed);
    }
    else
    {
      SetSpeed_Grounded_Friction(frictionSpeed, groundSpeedDeadZone);
    }

    SpeedX = _groundSpeed * MathF.Cos(groundAngleRad);
    SpeedY = _groundSpeed * MathF.Sin(groundAngleRad);
  }

  private void SetSpeed_Grounded_Forward(
    float topSpeed,
    float accelerationSpeed,
    float decelerationSpeed)
  {
    if (_groundSpeed < 0)
    {
      _groundSpeed += decelerationSpeed;

      if (_groundSpeed >= 0)
      {
        _groundSpeed = decelerationSpeed;
      }
    }
    else if (_groundSpeed < topSpeed)
    {
      _groundSpeed += accelerationSpeed;

      if (_groundSpeed >= topSpeed)
      {
        _groundSpeed = topSpeed;
      }
    }
  }

  private void SetSpeed_Grounded_Back(
    float topSpeed,
    float accelerationSpeed,
    float decelerationSpeed)
  {
    if (_groundSpeed > 0)
    {
      _groundSpeed -= decelerationSpeed;

      if (_groundSpeed <= 0)
      {
        _groundSpeed = -decelerationSpeed;
      }
    }
    else if (_groundSpeed > -topSpeed)
    {
      _groundSpeed -= accelerationSpeed;

      if (_groundSpeed <= -topSpeed)
      {
        _groundSpeed = -topSpeed;
      }
    }
  }

  public void SetSpeed_Grounded_Friction(
    float frictionSpeed,
    float groundSpeedDeadZone)
  {
    if (Mathf.Abs(_groundSpeed) < groundSpeedDeadZone)
    {
      _groundSpeed = 0;
      return;
    }

    _groundSpeed -= frictionSpeed * Mathf.Sign(_groundSpeed);
  }
}
