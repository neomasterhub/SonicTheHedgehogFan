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
    float gravityUp,
    float gravityDown,
    float maxFallSpeed,
    float groundSpeedDeadZone,
    float airTopSpeed,
    float airAccelerationSpeed,
    float groundSensorLength,
    float distanceToGround,
    float inputDeadZone)
  {
    if (playerState.HasFlag(PlayerState.Airborne))
    {
      SetSpeed_Airborne(gravityUp, gravityDown, maxFallSpeed, airTopSpeed, airAccelerationSpeed, inputDeadZone, groundSensorLength, distanceToGround);
    }
    else if (playerState.HasFlag(PlayerState.Grounded))
    {
      SetSpeed_Grounded(topSpeed, accelerationSpeed, decelerationSpeed, frictionSpeed, groundSpeedDeadZone, groundAngleRad);
    }
  }

  private void SetSpeed_Airborne(
    float gravityUp,
    float gravityDown,
    float maxFallSpeed,
    float airTopSpeed,
    float airAccelerationSpeed,
    float groundSensorLength,
    float distanceToGround,
    float inputDeadZone)
  {
    SetSpeed_Airborne_Gravity(gravityUp, gravityDown, maxFallSpeed);
    SetSpeed_Airborne_PreventGroundOvershoot(groundSensorLength, distanceToGround);
    SetSpeed_Airborne_Horizontal(airTopSpeed, airAccelerationSpeed, inputDeadZone);
  }

  private void SetSpeed_Airborne_Gravity(
    float gravityUp,
    float gravityDown,
    float maxFallSpeed)
  {
    var g = SpeedY > 0 ? gravityUp : gravityDown;
    SpeedY -= g;

    if (SpeedY < -maxFallSpeed)
    {
      SpeedY = -maxFallSpeed;
    }
  }

  private void SetSpeed_Airborne_PreventGroundOvershoot(
    float groundSensorLength,
    float distanceToGround)
  {
    if (SpeedY > 0)
    {
      return;
    }

    // Keeps surface normal aligned with slope.
    var yPositionOffset = groundSensorLength / 2;

    var maxFallStep = distanceToGround - yPositionOffset;

    if (SpeedY < -maxFallStep)
    {
      SpeedY = -maxFallStep;
    }
  }

  private void SetSpeed_Airborne_Horizontal(
    float airTopSpeed,
    float airAccelerationSpeed,
    float inputDeadZone)
  {
    if (Mathf.Abs(_inputInfo.X) < inputDeadZone)
    {
      return;
    }

    SpeedX += _inputInfo.X * airAccelerationSpeed;

    if (Mathf.Abs(SpeedX) > airTopSpeed)
    {
      SpeedX = airTopSpeed * Mathf.Sign(SpeedX);
    }
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
      SetSpeed_Grounded_Backward(topSpeed, accelerationSpeed, decelerationSpeed);
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

  private void SetSpeed_Grounded_Backward(
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
