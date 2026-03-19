using System.Collections.Generic;
using System.Linq;

public class PlayerViewRotatorProvider
{
  private readonly HashSet<IPlayerViewRotator> _rotators = new();

  public IPlayerViewRotator First()
  {
    return _rotators.FirstOrDefault(r => r.Condition());
  }

  public PlayerViewRotatorProvider Add(IPlayerViewRotator rotator)
  {
    _rotators.Add(rotator);
    return this;
  }
}
