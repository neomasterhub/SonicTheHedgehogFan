public interface IMotor : IEnemySpeedSystem
{
  MotorState State { get; }
  void Idle();
  void Drive();
  void SetSpeed(MotorContext context);
}
