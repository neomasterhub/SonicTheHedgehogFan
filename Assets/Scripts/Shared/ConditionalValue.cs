using System;

public readonly struct ConditionalValue<TValue>
{
  public readonly Func<bool> Condition;
  public readonly Func<TValue> Provider;

  public ConditionalValue(Func<bool> condition, Func<TValue> provider)
  {
    Condition = condition;
    Provider = provider;
  }
}
