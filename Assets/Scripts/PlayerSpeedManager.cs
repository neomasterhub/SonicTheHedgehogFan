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

  public void SetSpeed(PlayerState playerState, float groundAngleRad)
  {
    if (playerState.HasFlag(PlayerState.Airborne))
    {
      SetSpeed_Airborne();
    }
    else if (playerState.HasFlag(PlayerState.Grounded))
    {
      SetSpeed_Grounded();
    }
  }

  private void SetSpeed_Airborne()
  {
  }

  private void SetSpeed_Grounded()
  {
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
}
