using UnityEngine;
using static SharedConsts;
using static SpinSmokeConsts;

/// <summary>
/// Init.
/// </summary>
public partial class SpinSmokeController
{
  public SpinSmokeController()
  {
    _alphaMax = AlphaMax;
    _alphaStep = AlphaStep;
  }

  private void Awake()
  {
    _particleSystem = GetComponent<ParticleSystem>();
    _source = GameObject.FindWithTag(Tags.Player).GetComponent<ISpinSmokeSource>();
  }
}
