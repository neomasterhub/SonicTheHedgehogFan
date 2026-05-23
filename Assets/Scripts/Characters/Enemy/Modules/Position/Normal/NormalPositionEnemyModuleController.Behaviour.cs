using UnityEngine;
using static SharedConsts.Physics;

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
      var ground = _context.Ground.Value;

      speedY -= (ground.Distance
        * (int)ground.SensorGroundRelation)
        - GroundedPositionUpwardOffset;
    }

    transform.position += new Vector3(_context.SpeedX, speedY);
  }
}
