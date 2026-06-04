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
