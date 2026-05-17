/// <summary>
/// Init.
/// </summary>
public partial class MotobugController
{
  public MotobugController()
    : base(new MotobugSpeedSystem())
  {
    _viewSystem = new();
  }
}
