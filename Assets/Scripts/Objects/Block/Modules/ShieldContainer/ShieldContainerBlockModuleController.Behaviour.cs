/// <summary>
/// Behaviour.
/// </summary>
public partial class ShieldContainerBlockModuleController
{
  public override void Apply()
  {
    if (_context.IsDestroyed)
    {
      _context.Player.ShieldReceived = true;
    }
  }
}
