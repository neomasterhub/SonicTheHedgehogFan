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
    ApplyMovement();
  }

  private void BeginFrame()
  {
    _timerSystem.Update(Time.fixedDeltaTime);
  }

  private void ApplyEffects()
  {
    _effects.Run(false);
  }

  private void ApplyMovement()
  {
    _motor.Move();
  }
}
