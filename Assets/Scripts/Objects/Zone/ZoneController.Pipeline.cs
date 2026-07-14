/// <summary>
/// Pipeline.
/// </summary>
public partial class ZoneController
{
  private void LateUpdate()
  {
    SetIntersectingZone();
    DrawZone();
  }

  private void SetIntersectingZone()
  {
    _zoneObject.IntersectingZones = _zoneObject.IntersectingZones.Set(
      _zoneType,
      _zoneCollider.bounds.Intersects(_zoneObjectCollider.bounds));
  }

  private void DrawZone()
  {
    if (_debugMode)
    {
      _drawZoneCollider.Invoke();
    }
  }
}
