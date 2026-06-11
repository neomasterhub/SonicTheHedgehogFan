public interface IBlockPlayer
{
  bool IsGrounded { get; }
  float PositionX { get; }
  float SpeedX { get; }
  IBlock ContactBlock { get; set; }
}
