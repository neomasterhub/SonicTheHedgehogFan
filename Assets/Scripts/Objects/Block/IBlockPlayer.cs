public interface IBlockPlayer
{
  bool IsGrounded { get; }
  bool PrevIsGrounded { get; }
  float PositionX { get; }
  float SpeedX { get; }
  float? HDistanceToBlock { set; }
  IBlock ContactBlock { get; set; }
}
