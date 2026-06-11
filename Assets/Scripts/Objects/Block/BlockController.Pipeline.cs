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
  }

  private void ApplyEffects()
  {
    _effects.Run();
  }

  private void ApplyMovement()
  {
    transform.position += PositionVector3(_player.SpeedX, 0);
  }
}
