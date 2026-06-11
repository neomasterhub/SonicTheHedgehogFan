using static Helpers.Math;

/// <summary>
/// Pipeline.
/// </summary>
public partial class BlockController
{
  private void FixedUpdate()
  {
    ApplyEffects();
    ApplyMovement();
    UpdatePosition();
  }

  private void ApplyEffects()
  {
    _effects.Run();
  }

  private void ApplyMovement()
  {
    _speedSystem.SetSpeed(new(transform.position.x));
  }

  private void UpdatePosition()
  {
    transform.position += PositionVector3(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }
}
