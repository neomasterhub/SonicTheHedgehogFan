public interface IBlockPlayer
{
  bool IsGrounded { get; }
  bool PrevIsGrounded { get; }
  float PositionX { get; }
  float SpeedX { get; }
  IBlock ContactBlock { get; set; }
}
