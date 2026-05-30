/// <summary>
/// Debug.
/// </summary>
public partial class AnimalController
{
  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }
}
