using System.Linq;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class LevelSceneController
{
  private void Awake()
  {
    InitializeObjects();
  }

  private void InitializeObjects()
  {
    var objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

    _debugObjects = objects
      .OfType<ISceneObjectDebug>()
      .ToArray();
  }
}
