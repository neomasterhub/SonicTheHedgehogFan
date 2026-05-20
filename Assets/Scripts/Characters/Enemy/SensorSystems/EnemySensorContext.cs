using UnityEngine;

public readonly struct EnemySensorContext
{
  public readonly bool HorizontalDirection;
  public readonly Vector2 ParentPosition;

  public EnemySensorContext(bool horizontalDirection, Vector2 parentPosition)
  {
    HorizontalDirection = horizontalDirection;
    ParentPosition = parentPosition;
  }
}
