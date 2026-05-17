public interface IMotor : ISpeedSystem
{
  MotorState State { get; }
  void Idle();
  void Drive();
  void Update(MotorContext context);
}
