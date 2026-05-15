using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private readonly Pipeline _effects;

  private bool _isFadingOut;
  private CinemachineCamera _cm;
  private ICameraTarget _target;

  [SerializeField]
  private GameObject _targetObj;
}
