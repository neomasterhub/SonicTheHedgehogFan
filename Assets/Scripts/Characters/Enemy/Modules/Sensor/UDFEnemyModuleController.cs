using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class UDFEnemyModuleController : EnemyModuleControllerBase
{
  private UDFSensor _o;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private Vector2 _position;
}
