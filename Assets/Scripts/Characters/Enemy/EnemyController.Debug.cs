/// <summary>
/// Debug.
/// </summary>
public partial class EnemyController
{
  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }
}
