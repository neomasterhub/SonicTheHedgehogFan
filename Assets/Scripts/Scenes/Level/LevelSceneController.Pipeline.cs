using static SharedConsts.SecretCodes;

/// <summary>
/// Pipeline.
/// </summary>
public partial class LevelSceneController
{
  private void FixedUpdate()
  {
    UpdateInput();
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
    }
  }
}
