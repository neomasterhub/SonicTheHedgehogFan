/// <summary>
/// Behaviour.
/// </summary>
public partial class SpeedShoesContainerBlockModuleController
{
  public override void Apply()
  {
    if (_context.IsDestroyed)
    {
      _context.Player.SpeedShoesReceived = true;
    }
  }
}
