using static RingConsts.Physics;

public class RingConfigs
{
  public RingConfigs(PhysicsMode physicsMode)
  {
    Set(physicsMode);
  }

  public PhysicsMode PhysicsMode { get; private set; }
  public RingPhysicsModeConfig PhysicsModeConfig { get; private set; }

  public void Update(PhysicsMode physicsMode)
  {
    if (physicsMode != PhysicsMode)
    {
      Set(physicsMode);
    }
  }

  private void Set(PhysicsMode physicsMode)
  {
    PhysicsMode = physicsMode;

    PhysicsModeConfig = physicsMode switch
    {
      PhysicsMode.Normal => NormalConfig,
      _ => throw physicsMode.ArgumentOutOfRangeException(),
    };
  }
}
