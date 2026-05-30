using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class OffscreenDeactivatorController
{
  public OffscreenDeactivatorController()
  {
    _offscreenCheckInterval = 1;
  }

  private void Awake()
  {
    _camera = _cameraObj.GetComponent<Camera>();
  }
}
