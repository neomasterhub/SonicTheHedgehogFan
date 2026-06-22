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
    EndFrame();
  }

  private void BeginFrame()
  {
    PositionX = transform.position.x;
    PositionY = transform.position.y;

    if (_otherEnemy.ContactEnemy == null)
    {
      IsHit = false;
    }
  }

  private void ApplyEffects()
  {
    _effects.Run();
  }

  private void ApplyModules()
  {
    for (var i = 0; i < _modules.Length; i++)
    {
      _modules[i].Apply();
    }
  }

  private void EndFrame()
  {
    _reboundSignal = null;
  }
}
