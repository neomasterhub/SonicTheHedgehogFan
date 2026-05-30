using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class OffscreenDeactivatorController : MonoBehaviour
{
  private float _screenHalfWidth;
  private float _screenHalfHeight;
  private Transform _cameraTransform;

  [SerializeField]
  private float _checkInterval;
  [SerializeField]
  [InspectorLabel("Camera")]
  private GameObject _cameraObj;
}
