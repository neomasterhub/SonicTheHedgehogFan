using UnityEngine;
using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class UDFEnemySensorSystemController
{
  private void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _o = new(Color.red, _position, Vector2.up, Vector2.down, _spriteRenderer.flipX ? Vector2.left : Vector2.right);
    _o.UpRay.Length = UDFLengths.x;
    _o.DownRay.Length = UDFLengths.y;
    _o.FrontRay.Length = UDFLengths.z;
  }
}
