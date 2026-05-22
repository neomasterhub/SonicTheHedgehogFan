using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public partial class UDFSensorEnemyModuleController
  : EnemyModuleControllerBase
{
  private UDFSensor _o;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private Vector2 _position;
  [SerializeField]
  private Vector3 _udfLengths;
}
