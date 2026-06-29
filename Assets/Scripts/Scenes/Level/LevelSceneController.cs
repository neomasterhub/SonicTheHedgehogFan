using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class LevelSceneController : SceneControllerBase
{
  private readonly StringBuilder _diagnosticsText;

  private bool _prevDebugMode;
  private bool _playerHasInvincibilityStars;
  private bool _prevPlayerHasInvincibilityStars;
  private int _soundCount;
  private ISceneObjectDebug[] _debugObjects;
  private ISceneObjectPlayer _player;
  private GameObject _diagnosticsPanel;
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
