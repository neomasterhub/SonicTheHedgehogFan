/// <summary>
/// Pipeline.
/// </summary>
public partial class ZoneController
{
  private void LateUpdate()
  {
    _zoneObject.IntersectingZones = _zoneObject.IntersectingZones.Set(
      _zoneType,
      _zoneCollider.bounds.Intersects(_zoneObjectCollider.bounds));
  }
}
