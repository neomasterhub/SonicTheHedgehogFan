using static Helpers.Diagnostics;

/// <summary>
/// Debug.
/// </summary>
public partial class LevelSceneController
{
  private void UpdateDebug()
  {
    if (_debugMode != _prevDebugMode)
    {
      UpdateDebug_Toggle();
    }

    if (_debugMode)
    {
      UpdateDebug_Diagnostics();
    }
  }

  private void UpdateDebug_Toggle()
  {
    _diagnosticsPanel.SetActive(_debugMode);
  }

  private void UpdateDebug_Diagnostics()
  {
    _diagnosticsText
      .Clear()
      .AppendLine($"FPS {GetFPS()}")
      ;

    _diagnosticsTextMesh.SetText(_diagnosticsText);
  }
}
