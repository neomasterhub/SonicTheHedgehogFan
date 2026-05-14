using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private CinemachineCamera _cm;
  private ICameraTarget _target;

  [SerializeField]
  private GameObject _targetObj;
}
