using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class AnimalController : IAnimal
{
  public void BreakOut(
    Vector2 position,
    bool horizontalDirection)
  {
    _horizontalSpeedPx = horizontalDirection ? _horizontalSpeedPx : -_horizontalSpeedPx;

    transform.position = position;

    gameObject.SetActive(true);
  }
}
