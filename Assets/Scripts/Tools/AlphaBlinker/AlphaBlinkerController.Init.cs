using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class AlphaBlinkerController
{
  public AlphaBlinkerController()
  {
    _alphaBlinker = new();
    _interval = 0.1f;
  }

  private void Awake()
  {
    _alphaBlinker.SetComponent(GetComponent<SpriteRenderer>());
    _alphaBlinker.Start(_alpha, float.PositiveInfinity, _interval);
  }
}
