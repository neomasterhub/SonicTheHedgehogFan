using UnityEngine;
using static Helpers.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class NormalPositionEnemyModuleController
{
  public override void Apply()
  {
    SetPosition();
  }

  private void SetPosition()
  {
    var speedY = _context.SpeedY;

    if (_context.Ground.HasValue)
    {
      speedY -= GetGroundClearance(_context.Ground.Value);
    }

    transform.position += new Vector3(_context.SpeedX, speedY);
  }
}
