using System.Linq;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class PlatformController
{
  private void Awake()
  {
    InitializeData();
    InitializeComponents();
    InitializePlatformObjects();
  }

  private void InitializeData()
  {
    _prevPosition = transform.position;
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
