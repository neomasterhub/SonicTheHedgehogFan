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
    _motor.SetSpeed(_motorContext);
  }

  private void UpdatePosition()
  {
    transform.position += new Vector3(_motor.SpeedX.Round(SpeedRoundingDigits), _motor.SpeedY.Round(SpeedRoundingDigits));
  }
}
