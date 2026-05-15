using UnityEngine;

/// <summary>
/// Effects.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_FadeOut());
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

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
