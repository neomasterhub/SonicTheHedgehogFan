using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class UDFSensorEnemyModuleController : EnemyModuleControllerBase
{
  private UDFSensor _o;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private Vector2 _position;
}
