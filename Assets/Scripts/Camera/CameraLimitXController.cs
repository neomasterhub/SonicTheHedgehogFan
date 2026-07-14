using Unity.Cinemachine;
using UnityEngine;

[ExecuteAlways]
public class CameraLimitXController : CinemachineExtension
{
  [SerializeField]
  private float _xMin;
  [SerializeField]
  private float _xMax;

  protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
  {
    base.PostPipelineStageCallback(vcam, stage, ref state, deltaTime);

    if (stage == CinemachineCore.Stage.Finalize)
    {
      var pos = state.RawPosition;

      if (pos.x < _xMin)
      {
        pos.x = _xMin;
      }
      else if (pos.x > _xMax)
      {
        pos.x = _xMax;
      }

      state.RawPosition = pos;
    }
  }
}
