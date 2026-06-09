/// <summary>
/// Pipeline.
/// </summary>
public partial class PlatformController
{
  private void FixedUpdate()
  {
    ApplyModules();
    SetContact();
  }

  private void SetContact()
  {
    for (var i = 0; i < _platformObjects.Length; i++)
    {
      var obj = _platformObjects[i];

      if (obj.ContactTransform == transform)
      {
        obj.ContactPlatform = this;
      }
    }
  }

  private void ApplyModules()
  {
    for (var i = 0; i < _modules.Length; i++)
    {
      _modules[i].Apply();
    }
  }
}
