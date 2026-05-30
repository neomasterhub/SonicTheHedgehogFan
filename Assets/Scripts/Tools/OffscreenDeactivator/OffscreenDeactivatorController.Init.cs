using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class OffscreenDeactivatorController
{
  public OffscreenDeactivatorController()
  {
    _timerSystem = new();
    _offscreenCheckInterval = 1;
  }

  private void Awake()
  {
    _camera = _cameraObj.GetComponent<Camera>();

    _isActive = true;

    _activeTimer = new Timer(_offscreenCheckInterval)
      .WhenCompleted(() =>
      {
        var viewportPos = _camera.WorldToViewportPoint(transform.position);

        _isActive = viewportPos.x >= 0
          && viewportPos.x <= 1
          && viewportPos.y >= 0
          && viewportPos.y <= 1;
      });
  }
}
