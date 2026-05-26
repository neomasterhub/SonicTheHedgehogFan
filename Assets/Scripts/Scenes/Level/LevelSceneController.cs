using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class LevelSceneController : SceneControllerBase
{
  private ISceneObjectDebug[] _debugObjects;

  [SerializeField]
  private bool _debugMode;
}
