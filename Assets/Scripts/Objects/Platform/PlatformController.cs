using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public partial class PlatformController : PlatformControllerBase
{
  private Collider2D _collider;
  private IPlatformObject[] _platformObjects;
}
