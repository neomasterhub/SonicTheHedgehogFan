using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachinePositionComposer))]
public class CameraLookVertical : MonoBehaviour
{
  private const float _yLimit = 0.45f;
  private const float _yStep = 0.5f;

  private CinemachinePositionComposer _camPos;
  private ILookVerticalDirectionProvider _directionProvider;

  public GameObject DirectionProvider;

  private void Awake()
  {
    _camPos = GetComponent<CinemachinePositionComposer>();
    _directionProvider = DirectionProvider.GetComponent<ILookVerticalDirectionProvider>();
  }

  private void Update()
  {
  }
}
