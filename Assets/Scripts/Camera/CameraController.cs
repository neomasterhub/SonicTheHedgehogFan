using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Data.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private readonly Pipeline _effects;

  private bool _isFadingOut;
  private CinemachineCamera _cm;
  private ICameraTarget _target;
  private GameObject _overlayPanelObj;
  private Image _overlayPanelImage;

  [SerializeField]
  private Canvas _canvas;
  [SerializeField]
  [InspectorLabel("Target")]
  private GameObject _targetObj;
}
