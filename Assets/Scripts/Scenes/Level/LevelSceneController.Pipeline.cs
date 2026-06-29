using static SharedConsts.SecretCodes;

/// <summary>
/// Pipeline.
/// </summary>
public partial class LevelSceneController
{
  private void Update()
  {
    UpdateDebug();
  }

  private void FixedUpdate()
  {
    BeginFrame();
    UpdateInput();
    UpdateSounds();
  }

  private void BeginFrame()
  {
    _prevDebugMode = _debugMode;

    _prevPlayerHasInvincibilityStars = _playerHasInvincibilityStars;
    _playerHasInvincibilityStars = _player.HasInvincibilityStars;
  }

  private void UpdateInput()
  {
    _inputSystem.Update();

    if (_inputSystem.Pressed == PlayerInput.None)
    {
      return;
    }

    if (_inputSystem.CheckLastPressed(ToggleDebugMode))
    {
      _debugMode = !_debugMode;
      for (var i = 0; i < _debugObjects.Length; i++)
      {
        _debugObjects[i].DebugMode = _debugMode;
      }
    }
  }

  private void UpdateSounds()
  {
    for (var i = 0; i < _soundCount; i++)
    {
      _sounds[i].Stop().Play();
    }
  }
}
