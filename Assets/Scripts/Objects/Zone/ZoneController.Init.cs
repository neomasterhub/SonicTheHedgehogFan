using UnityEngine;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class ZoneController
{
  private void Awake()
  {
    if (_zoneObjectObj == null)
    {
      _zoneObjectObj = GameObject.FindWithTag(Tags.Player);
    }

    _zoneObjectCollider = _zoneObjectObj.GetComponent<Collider2D>();
    _zoneObject = _zoneObjectObj.GetComponent<IZoneObject>();
    _zoneCollider = GetComponent<Collider2D>();
  }
}
