using System.Collections.Generic;
using System.Linq;

public class PlayerViewRotatorProvider
{
  private readonly HashSet<IPlayerViewRotator> _rotators = new();

  public IPlayerViewRotator Current { get; private set; }
  public IPlayerViewRotator Default { get; set; }

  public PlayerViewRotatorProvider Add(IPlayerViewRotator rotator)
  {
    _rotators.Add(rotator);
    return this;
  }

  public IPlayerViewRotator FirstTriggered()
  {
    Current = _rotators.FirstOrDefault(r => r.Condition());
    return Current;
  }

  public IPlayerViewRotator FirstTriggeredOrDefault()
  {
    Current = FirstTriggered() ?? Default;
    return Current;
  }
}
