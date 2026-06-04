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
    _collider = GetComponent<Collider2D>();
  }

  private void InitializePlatformObjects()
  {
    _platformObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
      .OfType<IPlatformObject>()
      .ToArray();
  }
}
