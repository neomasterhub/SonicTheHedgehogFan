using System;

public static class ObjectExtensions
{
  public static ArgumentOutOfRangeException ArgumentOutOfRangeException<TArg>(this TArg arg)
  {
    return new ArgumentOutOfRangeException(
      nameof(arg),
      arg,
      $"Invalid argument of type {typeof(TArg).Name}: {arg}");
  }
}
