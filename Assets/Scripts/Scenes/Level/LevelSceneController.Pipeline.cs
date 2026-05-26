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
  }

  private void BeginFrame()
  {
    _prevDebugMode = _debugMode;
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
}
