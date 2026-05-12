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

    ApplyEffects();
  }

  private void ApplyEffects()
  {
    _effects.Run(false);
  }
}
