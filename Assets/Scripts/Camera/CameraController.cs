using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Data.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private readonly Pipeline _effects;

  private bool _isFadingOut;
  private CinemachineCamera _cm;
  private ICameraTarget _target;
  private IPanel _overlayPanel;

  [SerializeField]
  private Canvas _canvas;
  [SerializeField]
  [InspectorLabel("Target")]
  private GameObject _targetObj;
}
