using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public partial class AlphaBlinkerController : MonoBehaviour
{
  private readonly AlphaBlinker _alphaBlinker;

  [SerializeField]
  private float _alpha;
  [SerializeField]
  private float _interval;
}
