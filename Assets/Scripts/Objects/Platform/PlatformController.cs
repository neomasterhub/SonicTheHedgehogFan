using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[DefaultExecutionOrder(-1)]
public partial class PlatformController : PlatformControllerBase
{
  private IPlatformObject[] _platformObjects;
  private PlatformModuleControllerBase[] _modules;
}
