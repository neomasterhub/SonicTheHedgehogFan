using UnityEngine;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class SpinSmokeController
{
  private void Awake()
  {
    _particleSystem = GetComponent<ParticleSystem>();
    _source = GameObject.FindWithTag(Tags.Player).GetComponent<ISpinSmokeSource>();
  }
}
