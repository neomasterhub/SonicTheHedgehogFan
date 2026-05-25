using System.Collections.Generic;

public abstract class SoundEnemyModuleControllerBase
  : EnemyModuleControllerBase
{
  protected readonly List<Sound> _sounds;

  protected SoundEnemyModuleControllerBase()
    : base()
  {
    _sounds = new();
  }

  protected abstract void SetSounds();

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    SetSounds();
  }
}
