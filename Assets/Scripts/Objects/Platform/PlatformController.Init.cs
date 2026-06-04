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
    if (_platformObjectPrefabs == null
      || _platformObjectPrefabs.Length == 0)
    {
      _platformObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
        .OfType<IPlatformObject>()
        .ToArray();
    }
  }
}
