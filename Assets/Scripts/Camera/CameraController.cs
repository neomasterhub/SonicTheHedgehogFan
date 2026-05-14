using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private CinemachineCamera _cm;

  [SerializeField]
  private ICameraTarget _target;
}
