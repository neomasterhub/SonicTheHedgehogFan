using System;

public interface IEnemySystem<TType, TContext>
  : ITyped<TType>,
  IUpdatable<TContext>
  where TType : struct, Enum
{
}
