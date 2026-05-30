using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class OffscreenDeactivatorController : MonoBehaviour
{
  private readonly TimerSystem _timerSystem;

  private bool _isActive;
  private Camera _camera;
  private Timer _activeTimer;

  [SerializeField]
  private float _offscreenCheckInterval;
  [SerializeField]
  [InspectorLabel("Camera")]
  private GameObject _cameraObj;
}
