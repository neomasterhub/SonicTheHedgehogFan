using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class OffscreenDeactivatorController
{
  private void FixedUpdate()
  {
    _time += Time.fixedDeltaTime;

    if (_time < _checkInterval)
    {
      return;
    }

    _time = 0;

    var viewportPos = _camera.WorldToViewportPoint(transform.position);

    if (viewportPos.x < 0
      || viewportPos.x > 1
      || viewportPos.y < 0
      || viewportPos.y > 1)
    {
      gameObject.SetActive(false);
    }
  }
}
