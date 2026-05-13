using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class EnemyController : MonoBehaviour
{
  private readonly Pipeline _effects;

  private IEnemy _otherEnemy;
  private BoxCollider2D _collider;
  private BoxCollider2D _otherEnemyCollider;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private bool _initialized;
  [SerializeField]
  [InspectorLabel("Other Enemy")]
  private GameObject _otherEnemyGameObject;
}
