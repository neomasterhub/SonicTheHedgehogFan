using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class LevelSceneController : SceneControllerBase
{
  private bool _prevDebugMode;
  private ISceneObjectDebug[] _debugObjects;
  private GameObject _diagnosticsPanel;
  private StringBuilder _diagnosticsText;
  private TextMeshProUGUI _diagnosticsTextMesh;

  [SerializeField]
  private Canvas _canvas;
  [SerializeField]
  private bool _debugMode;

  [Header("Audio")]
  [SerializeField]
  [InspectorLabel("Invincibility stars")]
  private AudioClip _invincibilityStarsAudioClip;
}
