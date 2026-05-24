using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public partial class EnemyController : EnemyControllerBase
{
  private readonly Pipeline _effects;

  private BoxCollider2D _collider;
  private BoxCollider2D _otherEnemyCollider;
  private EnemyModuleControllerBase[] _modules;
  private IEnemy _otherEnemy;

  [SerializeField]
  [InspectorLabel("Alive")]
  private bool _isAlive;
  [SerializeField]
  [InspectorLabel("Other Enemy")]
  private GameObject _otherEnemyObj;
}
