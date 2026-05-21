using System;

public interface ITyped<TType>
  where TType : struct, Enum
{
  TType Type { get; }
}
