using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public partial class ZoneController : MonoBehaviour
{
  private Collider2D _zoneCollider;
  private Collider2D _zoneObjectCollider;
  private IZoneObject _zoneObject;

  [SerializeField]
  private ZoneType _zoneType;
  [SerializeField]
  private GameObject _zoneObjectObj;
}
