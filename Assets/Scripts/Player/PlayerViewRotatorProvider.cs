using System.Collections.Generic;

public class PlayerViewRotatorProvider<TContext>
  where TContext : struct
{
  private readonly List<IPlayerViewRotator<TContext>> _rotators = new();

  public IPlayerViewRotator<TContext> TriggeredRotator { get; private set; }
  public IPlayerViewRotator<TContext> DefaultRotator { get; set; }

  public PlayerViewRotatorProvider<TContext> Add(IPlayerViewRotator<TContext> rotator)
  {
    _rotators.Add(rotator);
    return this;
  }

  public IPlayerViewRotator<TContext> FirstTriggered()
  {
    for (var i = 0; i < _rotators.Count; i++)
    {
      var rotator = _rotators[i];
      if (rotator.Condition())
      {
        TriggeredRotator = rotator;
        return rotator;
      }
    }

    TriggeredRotator = DefaultRotator;

    return DefaultRotator;
  }
}
