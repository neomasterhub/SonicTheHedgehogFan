using System.Linq;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class PlatformController
{
  private void Awake()
  {
    InitializePlatformObjects();
  }

  private void InitializePlatformObjects()
  {
    _platformObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
      .OfType<IPlatformObject>()
      .ToArray();
  }
}
