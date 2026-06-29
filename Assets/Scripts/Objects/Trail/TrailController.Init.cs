/// <summary>
/// Init.
/// </summary>
public partial class TrailController
{
  private void Awake()
  {
    _target = transform.parent.transform;
    _position = _target.position;
  }
}
