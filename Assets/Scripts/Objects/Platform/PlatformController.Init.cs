using System.Linq;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class PlatformController
{
  private void Awake()
  {
    InitializeComponents();
    InitializePlatformObjects();
  }

  private void InitializeComponents()
  {
    _modules = GetComponents<PlatformModuleControllerBase>();
  }

  private void InitializePlatformObjects()
  {
    _platformObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
      .OfType<IPlatformObject>()
      .ToArray();
  }
}
