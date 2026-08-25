using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public partial class SpinSmokeController : MonoBehaviour, ISpinSmoke
{
  private readonly TimerSystem _timerSystem;

  private Color _color;
  private ISpinSmokeSource _source;
  private ParticleSystem _particleSystem;
  private Timer _destroyTimer;

  [SerializeField]
  private float _alphaMax;
  [SerializeField]
  private float _alphaInc;
  [SerializeField]
  private float _alphaDec;
}
