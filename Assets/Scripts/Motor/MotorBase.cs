using UnityEngine;

public abstract class MotorBase : MonoBehaviour, IMotor
{
  public MotorState State { get; protected set; }
  public float SpeedX { get; protected set; }
  public float SpeedY { get; protected set; }

  public void Drive()
  {
    State = MotorState.Drive;
  }

  public void Idle()
  {
    State = MotorState.Idle;
  }

  public abstract void SetSpeed(MotorContext context);
}
