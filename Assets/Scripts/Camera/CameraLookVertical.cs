using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachinePositionComposer))]
public class CameraLookVertical : MonoBehaviour
{
  private CinemachinePositionComposer _ccamPositionComposer;

  private void Awake()
  {
    _ccamPositionComposer = GetComponent<CinemachinePositionComposer>();
  }
}
