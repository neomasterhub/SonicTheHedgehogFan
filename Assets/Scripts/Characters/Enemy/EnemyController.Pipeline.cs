using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class EnemyController
{
  private void FixedUpdate()
  {
    if (!_initialized)
    {
      return;
    }

    BeginFrame();
    ApplyEffects();
  }

  private void BeginFrame()
  {
    _timerSystem.Update(Time.fixedDeltaTime);
  }

  private void ApplyEffects()
  {
    _effects.Run(false);
  }
}
