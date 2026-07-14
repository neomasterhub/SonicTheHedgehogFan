using Unity.Cinemachine;
using UnityEngine;

[ExecuteAlways]
public class CameraLimitYMinController : CinemachineExtension
{
  [SerializeField]
  private float _xMin;
  [SerializeField]
  private float _xMax;
  [SerializeField]
  private float _yMin;

  protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
  {
    base.PostPipelineStageCallback(vcam, stage, ref state, deltaTime);

    if (stage == CinemachineCore.Stage.Finalize)
    {
      var pos = state.RawPosition;
      if (_xMin <= pos.x && pos.x <= _xMax)
      {
        state.RawPosition = new(pos.x, Mathf.Max(pos.y, _yMin), pos.z);
      }
    }
  }
}
