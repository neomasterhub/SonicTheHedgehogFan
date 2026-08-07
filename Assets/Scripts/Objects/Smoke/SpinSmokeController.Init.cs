using UnityEngine;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class SpinSmokeController
{
  private void Awake()
  {
    _source = GameObject.FindWithTag(Tags.Player).GetComponent<ISpinSmokeSource>();
  }
}
