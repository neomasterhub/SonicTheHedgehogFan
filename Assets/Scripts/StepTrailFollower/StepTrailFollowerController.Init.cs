/// <summary>
/// Init.
/// </summary>
public partial class StepTrailFollowerController
{
  private void Awake()
  {
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    if (_parentTransform == null)
    {
      _parentTransform = gameObject.transform.parent.transform;
    }
  }
}
