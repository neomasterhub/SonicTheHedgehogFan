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
}
