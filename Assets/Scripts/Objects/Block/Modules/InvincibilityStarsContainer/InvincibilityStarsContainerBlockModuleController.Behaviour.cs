/// <summary>
/// Behaviour.
/// </summary>
public partial class InvincibilityStarsContainerBlockModuleController
{
  public override void Apply()
  {
    if (_context.IsDestroyed)
    {
      _context.Player.InvincibilityStarsReceived = true;
    }
  }
}
