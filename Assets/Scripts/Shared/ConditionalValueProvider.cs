using System;
using System.Collections.Generic;

public class ConditionalValueProvider<TValue>
{
  private readonly List<ConditionalValue<TValue>> _rules = new();

  public Func<TValue> DefaultProvider { get; set; } = () => default;

  public ConditionalValueProvider<TValue> When(Func<bool> condition, Func<TValue> provider)
  {
    _rules.Add(new(condition, provider));
    return this;
  }

  public TValue FirstTriggeredOrDefault()
  {
    for (var i = 0; i < _rules.Count; i++)
    {
      var rule = _rules[i];
      if (rule.Condition())
      {
        return rule.Provider();
      }
    }

    return DefaultProvider();
  }
}
