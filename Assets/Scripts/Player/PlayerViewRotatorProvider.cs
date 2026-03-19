using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerViewRotatorProvider
{
  private readonly List<IPlayerViewRotator> _provided = new();

  private readonly Dictionary<Func<PlayerViewRotatorInput, bool>, IPlayerViewRotator[]> _condition2rotators = new();

  public IReadOnlyList<IPlayerViewRotator> Provided => _provided;

  public IReadOnlyList<IPlayerViewRotator> this[PlayerViewRotatorInput input]
  {
    get
    {
      _provided.Clear();

      _provided.AddRange(_condition2rotators
        .Where(x => x.Key(input))
        .SelectMany(x => x.Value));

      return _provided;
    }
  }

  public PlayerViewRotatorProvider Add(
    Func<PlayerViewRotatorInput, bool> condition,
    params IPlayerViewRotator[] rotators)
  {
    _condition2rotators.Add(condition, rotators);

    return this;
  }
}
