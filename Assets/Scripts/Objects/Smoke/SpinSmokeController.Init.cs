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
    _timerSystem = new();

    _alphaMax = AlphaMax;
    _alphaInc = AlphaInc;
    _alphaDec = AlphaDec;
  }

  private void Awake()
  {
    _source = GameObject.FindWithTag(Tags.Player).GetComponent<ISpinSmokeSource>();

    _destroyTimer = new Timer(1)
      .WhenCompleted(() => Destroy(gameObject));

    InitializeParticleSystem();
  }

  private void InitializeParticleSystem()
  {
    _particleSystem = GetComponent<ParticleSystem>();
    var psm = _particleSystem.main;
    var color = psm.startColor.color;
    color.a = 0;
    psm.startColor = new ParticleSystem.MinMaxGradient(color);
  }
}
