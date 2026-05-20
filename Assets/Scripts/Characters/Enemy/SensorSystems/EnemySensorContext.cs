using UnityEngine;

public readonly struct EnemySensorContext
{
  public readonly Vector2 ParentPosition;

  public EnemySensorContext(Vector2 parentPosition)
  {
    ParentPosition = parentPosition;
  }
}
