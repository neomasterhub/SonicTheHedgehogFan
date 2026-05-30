using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class OffscreenDeactivatorController
{
  private void Start()
  {
    _timerSystem.StartIfNotRunning(_activeTimer);
  }

  private void LateUpdate()
  {
    _timerSystem.Update(Time.deltaTime);

    if (_activeTimer.IsCompleted)
    {
      if (_isActive)
      {
        _timerSystem.StartIfNotRunning(_activeTimer);
      }
      else
      {
        Deactivate();
      }
    }
  }

  private void Deactivate()
  {
    gameObject.SetActive(false);
  }
}
