using UnityEngine;
using static SharedConsts.Physics;

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
    UpdatePosition();
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
  }

  private void UpdatePosition()
  {
    transform.position += new Vector3(0f.Round(SpeedRoundingDigits), 0f.Round(SpeedRoundingDigits));
  }
}
