using UnityEngine;

/// <summary>
/// Effects.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_DisableFollowing());
    _effects.AddStep(CreateEffect_FadeOut());
  }

  private PipelineStep CreateEffect_DisableFollowing()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Disable following")
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

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
