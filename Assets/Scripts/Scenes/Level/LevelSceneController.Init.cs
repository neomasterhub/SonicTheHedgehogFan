using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    InitializeSounds();
  }

  private void InitializeObjects()
  {
    var objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
      .OfType<ISceneObject>()
      .ToArray();

    _debugObjects = objects
      .OfType<ISceneObjectDebug>()
      .ToArray();

    _player = (ISceneObjectPlayer)objects
      .Single(o => o is ISceneObjectPlayer);
  }

  private void InitializeComponents()
  {
    _diagnosticsPanel = _canvas.transform.Find("Scene Diagnostics Panel").gameObject;
    _diagnosticsTextMesh = _diagnosticsPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
  }

  private void InitializeSounds()
  {
    var invincibilityStars = this.AddComponent<AudioSource>();
    invincibilityStars.clip = _invincibilityStarsAudioClip;

    _sounds.Add(new(invincibilityStars,
      () => _playerHasInvincibilityStars && !_prevPlayerHasInvincibilityStars,
      () => !_playerHasInvincibilityStars && _prevPlayerHasInvincibilityStars));

    _soundCount = _sounds.Count;
  }
}
