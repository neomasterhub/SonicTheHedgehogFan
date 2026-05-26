using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class LevelSceneController
{
  public LevelSceneController()
    : base()
  {
    _diagnosticsText = new();
  }

  private void Awake()
  {
    InitializeObjects();
    InitializeComponents();
  }

  private void InitializeObjects()
  {
    var objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

    _debugObjects = objects
      .OfType<ISceneObjectDebug>()
      .ToArray();
  }

  private void InitializeComponents()
  {
    _diagnosticsPanel = _canvas.transform.Find("Scene Diagnostics Panel").gameObject;
    _diagnosticsTextMesh = _diagnosticsPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
  }
}
