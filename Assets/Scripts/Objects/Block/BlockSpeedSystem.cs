public class BlockSpeedSystem : SpeedSystemBase
{
  private IBlockPlayer _player;

  public void Initialize(IBlockPlayer player)
  {
    _player = player;
  }

  public void SetSpeed(BlockSpeedContext context)
  {
    if (_player.ContactBlock == null
      || !_player.IsGrounded)
    {
      SpeedX = 0;
      return;
    }

    if ((_player.PositionX < context.PositionX && _player.SpeedX > 0)
      || (_player.PositionX > context.PositionX && _player.SpeedX < 0))
    {
      SpeedX = _player.SpeedX;
    }
  }
}
