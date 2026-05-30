using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class OffscreenDeactivatorController : MonoBehaviour
{
  private float _time;
  private Camera _camera;

  [SerializeField]
  private float _checkInterval;
  [SerializeField]
  [InspectorLabel("Camera")]
  private GameObject _cameraObj;
}
