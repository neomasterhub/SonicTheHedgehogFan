using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public partial class SpinSmokeController : MonoBehaviour, ISpinSmoke
{
  private ISpinSmokeSource _source;
  private ParticleSystem _particleSystem;

  [SerializeField]
  private float _alphaMax;
  [SerializeField]
  private float _alphaStep;
}
