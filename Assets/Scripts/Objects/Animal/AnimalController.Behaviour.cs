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
    _speedXPx = horizontalDirection ? _speedXPx : -_speedXPx;

    transform.position = position;

    gameObject.SetActive(true);
  }
}
