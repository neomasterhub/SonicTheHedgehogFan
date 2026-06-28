/// <summary>
/// Behaviour.
/// </summary>
public partial class RingContainerBlockModuleController
{
  public override void Apply()
  {
    if (_context.IsDestroyed)
    {
      _context.Player.RingReceived = true;
      _ringCollector.Rings.Add(_ringCount);
      enabled = false;
    }
  }
}
