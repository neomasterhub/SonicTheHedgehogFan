using UnityEngine;
using static SharedConsts.Colors;

/// <summary>
/// Effects.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_RemoveTrackingTarget());
    _effects.AddStep(CreateEffect_FadeOut());
  }

  private PipelineStep CreateEffect_RemoveTrackingTarget()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Remove tracking target")
      .WithCondition(() =>
        _target.IsDead)
      .WithAction(() =>
      {
        _cm.Target.TrackingTarget = null;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_FadeOut()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Fade out")
      .WithCondition(() =>
        !_isFadingOut
        && _target.IsDead)
      .WithAction(() =>
      {
        _isFadingOut = true;
        _overlayPanelObj.SetActive(true);
        _overlayPanelImage.color = TransparentBlack;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
