using static SharedConsts.Rendering;

/// <summary>
/// Debug.
/// </summary>
public partial class SonicController
{
  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }

  private void UpdateDebugInfo()
  {
    _groundNormal.enabled = _showDebugInfo;
    _diagnosticsPanel.SetActive(_showDebugInfo);
    _effectHistoryPanel.SetActive(_showDebugInfo);

    if (!_showDebugInfo)
    {
      return;
    }

    UpdateGroundNormal();
    UpdateDebugInfo_Diagnostics();
    UpdateDebugInfo_EffectHistory();
  }

  private void UpdateGroundNormal()
  {
    if (!_isGrounded)
    {
      return;
    }

    _groundNormal.SetPosition(0, _lastGroundDetectionResult.Contact);
    _groundNormal.SetPosition(1, _lastGroundDetectionResult.Contact + _lastGroundDetectionResult.Normal);
  }

  private void UpdateDebugInfo_EffectHistory()
  {
    _effectHistoryText.Clear();

    var effectHistory = _effects.GetAppliedHistory();
    for (var i = 0; i < effectHistory.Length; i++)
    {
      _effectHistoryText.AppendLine(effectHistory[i].ToEffectString());
    }

    _effectHistoryTextMesh.SetText(_effectHistoryText);
  }

  private void UpdateDebugInfo_Diagnostics()
  {
    _diagnosticsText
      .Clear()
      .AppendLine($"ENV {(_isGrounded ? "Ground" : "Air")}")
      .AppendLine($"DPD {GetDpadState()}")
      .AppendLine($"INH {_inputSystem.GetPressedHistory()}")
      .AppendLine($"GR1 {_groundInfoSystem.Previous}")
      .AppendLine($"GR2 {_groundInfoSystem.Current}")
      .AppendLine($"GRV {_speedSystem.GravitySpeed * DebugScale:0}")
      .AppendLine($"SLF {_slopeFactor * DebugScale:0}")
      .AppendLine($"SLS {_speedSystem.SlopeSpeed * DebugScale:0;-0;0}")
      .AppendLine($"GRS {_speedSystem.GroundSpeed * DebugScale:0;-0;0}")
      .AppendLine($"SP {_speedSystem.SpeedX * DebugScale:0;-0;0} {_speedSystem.SpeedY * DebugScale:0;-0;0}")
      .AppendLine($"RT {_viewSystem.Rotator}")
      .AppendLine($"WL {GetWallInfo(_leftWallDetectionResult)}")
      .AppendLine($"WR {GetWallInfo(_rightWallDetectionResult)}")
      ;

    _diagnosticsTextMesh.SetText(_diagnosticsText);
  }

  private string GetDpadState()
  {
    if (_isFallingOffWall)
    {
      return "off";
    }

    if (_dpadLockTimer.IsRunning)
    {
      return $"off {_dpadLockTimer.RemainingSeconds:0.0000}";
    }

    return "on";
  }

  private string GetWallInfo(WallDetectionResult? wall)
  {
    if (wall == null)
    {
      return string.Empty;
    }

    return $"{wall.Value.AngleDeg:0;-0;0}° {wall.Value.Distance * DebugScale:0}";
  }
}
