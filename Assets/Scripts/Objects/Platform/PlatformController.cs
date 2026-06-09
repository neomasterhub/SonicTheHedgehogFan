using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public partial class PlatformController : PlatformControllerBase
{
  private PlatformModuleControllerBase[] _modules;
  private IPlatformObject[] _platformObjects;
}
