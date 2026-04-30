using static SonicConsts.Physics;

public class SonicConfigs
{
  public SonicConfigs(PhysicsMode physicsMode)
  {
    Set(physicsMode);
  }

  public PhysicsMode PhysicsMode { get; private set; }
  public SonicPhysicsModeConfig PhysicsModeConfig { get; private set; }

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
