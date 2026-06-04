/// <summary>
/// Pipeline.
/// </summary>
public partial class PlatformController
{
  private void FixedUpdate()
  {
    SetContact();
  }

  private void SetContact()
  {
    for (var i = 0; i < _platformObjects.Length; i++)
    {
      var obj = _platformObjects[i];
      obj.ContactPlatform = _collider.bounds.Intersects(obj.Collider.bounds) ? this : null;
    }
  }
}
