using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class SpinSmokeController : MonoBehaviour
{
  private ISpinSmokeSource _source;

  [SerializeField]
  private float _alphaMax;
  [SerializeField]
  private float _alphaStep;
}
