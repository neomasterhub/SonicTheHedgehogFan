using System.Collections.Generic;
using System.Linq;

public class PlayerViewRotatorProvider<TContext>
  where TContext : struct
{
  private readonly HashSet<IPlayerViewRotator<TContext>> _rotators = new();

  public IPlayerViewRotator<TContext> Triggered { get; private set; }
  public IPlayerViewRotator<TContext> Default { get; set; }

  public PlayerViewRotatorProvider<TContext> Add(IPlayerViewRotator<TContext> rotator)
  {
    _rotators.Add(rotator);
    return this;
  }

  public IPlayerViewRotator<TContext> FirstTriggered()
  {
    var rotator = _rotators.FirstOrDefault(r => r.Condition());

    if (rotator != null)
    {
      Triggered = rotator;
    }

    return rotator;
  }
}
