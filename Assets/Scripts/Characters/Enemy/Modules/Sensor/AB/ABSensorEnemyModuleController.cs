using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public partial class ABSensorEnemyModuleController
  : EnemyModuleControllerBase
{
  private readonly UDFSensor _a;
  private readonly UDFSensor _b;

  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private Vector2 _position;
  [SerializeField]
  [InspectorLabel("A-B Distance")]
  private float _abDistance;
  [SerializeField]
  private float _wallClearance;
}
