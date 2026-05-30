using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class OffscreenDeactivatorController
{
  private void Awake()
  {
    var camera = _cameraObj.GetComponent<Camera>();
    _cameraTransform = camera.transform;
    _screenHalfHeight = camera.orthographicSize;
    _screenHalfWidth = _screenHalfHeight * camera.aspect;
  }
}
