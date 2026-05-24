/// <summary>
/// Pipeline.
/// </summary>
public partial class EnemyController
{
  private void FixedUpdate()
  {
    BeginFrame();
    ApplyEffects();
    ApplyModules();
  }

  private void BeginFrame()
  {
    PositionX = transform.position.x;
    PositionY = transform.position.y;
  }

  private void ApplyEffects()
  {
    _effects.Run(false);
  }

  private void ApplyModules()
  {
    for (var i = 0; i < _modules.Length; i++)
    {
      _modules[i].Apply();
    }
  }
}
