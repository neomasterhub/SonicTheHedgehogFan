using UnityEngine;
using static SharedConsts.Physics;

/// <summary>
/// Pipeline.
/// </summary>
public partial class PlatformController
{
  private void FixedUpdate()
  {
    ApplyMovement();
    UpdatePosition();
  }

  private void ApplyMovement()
  {
    transform.position = new Vector3(SpeedX.Round(PositionRoundingDigits), SpeedY.Round(PositionRoundingDigits));
  }

  private void UpdatePosition()
  {
    for (var i = 0; i < _platformObjects.Length; i++)
    {
      var obj = _platformObjects[i];

      if (!_collider.bounds.Intersects(obj.Collider.bounds))
      {
        return;
      }

      if (obj.IsGrounded)
      {
        obj.PlatformSpeedX = SpeedX;
        obj.PlatformSpeedY = SpeedY;
      }
    }
  }
}
