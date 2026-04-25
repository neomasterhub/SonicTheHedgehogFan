/// <summary>
/// Debug.
/// </summary>
public partial class SonicController
{
  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }

  public void UpdateDebugInfo()
  {
    _diagnosticsPanel.SetActive(_showDebugInfo);
    _effectHistoryPanel.SetActive(_showDebugInfo);

    if (!_showDebugInfo)
    {
      return;
    }

    UpdateDebugInfo_Diagnostics();
    UpdateDebugInfo_EffectHistory();
  }

  public void UpdateDebugInfo_EffectHistory()
  {
    _effectHistoryText.Clear();

    var effectHistory = _effects.GetAppliedHistory();
    for (var i = 0; i < effectHistory.Length; i++)
    {
      _effectHistoryText.AppendLine(effectHistory[i].ToEffectString());
    }

    _effectHistoryTextMesh.SetText(_effectHistoryText);
  }

  public void UpdateDebugInfo_Diagnostics()
  {
    _diagnosticsText
      .Clear()
      .AppendLine($"ENV {(_isGrounded ? "Ground" : "Air")}")
      .AppendLine()
      .AppendLine($"INP {GetInputState()}")
      .AppendLine($"INH {_inputSystem.GetPressedHistory()}")
      .AppendLine()
      .AppendLine($"GP {_groundInfoSystem.Previous}")
      .AppendLine($"GC {_groundInfoSystem.Current}")
      .AppendLine($"GS {_speedSystem.SlopeSpeed * 10000,5:0;-0;0} {_speedSystem.GroundSpeed * 10000,5:0;-0;0}")
      .AppendLine($"SP {_speedSystem.SpeedX * 10000,5:0;-0;0} {_speedSystem.SpeedY * 10000,5:0;-0;0}")
      .AppendLine($"SLF {_slopeFactor * 10000,5:0}")
      .AppendLine()
      .AppendLine($"RT {_viewSystem.Rotator}")
      .AppendLine($"WL {GetWallInfo(_leftWallDetectionResult)}")
      .AppendLine($"WR {GetWallInfo(_rightWallDetectionResult)}")
      ;

    _diagnosticsTextMesh.SetText(_diagnosticsText);
  }

  private string GetInputState()
  {
    if (_isFallingOffWall)
    {
      return "OFF";
    }

    if (_inputUnlockTimer.IsRunning)
    {
      return $"OFF {_inputUnlockTimer.RemainingSeconds:0.0000}";
    }

    return "ON";
  }

  private string GetWallInfo(WallDetectionResult? wall)
  {
    if (wall == null)
    {
      return string.Empty;
    }

    return $"{wall.Value.AngleDeg,5:0;-0;0} {wall.Value.Distance * 10000,5:0}";
  }
}
