using System;
using System.Collections.Generic;
using System.Linq;

public class ConditionalValueProvider<TValue>
{
  private readonly List<(Func<bool> condition, Func<TValue> provider)> _rules = new();

  public Func<TValue> DefaultProvider { get; set; } = () => default;

  public ConditionalValueProvider<TValue> When(Func<bool> condition, Func<TValue> provider)
  {
    _rules.Add((condition, provider));
    return this;
  }

  public TValue FirstTriggeredOrDefault()
  {
    return (_rules.FirstOrDefault(x => x.condition()).provider ?? DefaultProvider)();
  }
}
