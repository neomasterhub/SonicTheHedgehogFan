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
    _horizontalDirection = horizontalDirection;
    transform.position = position;
    gameObject.SetActive(true);
  }
}
