#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif
using static SharedConsts.Rendering;
using static SonicConsts.Debug;
using static SonicConsts.Debug.ShortVideoMode;

/// <summary>
/// Debug.
/// </summary>
public partial class SonicController
{
  private void LateUpdate()
  {
    if (_debugMode)
    {
      DrawDebugTools();
    }
  }

  private void Debug()
  {
    if (_debugMode != _prevDebugMode)
    {
      Debug_Toggle();
    }

    if (_debugMode)
    {
      Debug_ShiftPanels();
      Debug_UpdateDiagnostics();
      Debug_UpdateEffectHistory();
    }

#if UNITY_EDITOR
    UpdateDebug_PauseEditor();
#endif
  }

  private void Debug_Toggle()
  {
    _diagnosticsPanel.SetActive(_debugMode);
    _effectHistoryPanel.SetActive(_debugMode);
  }

  private void Debug_ShiftPanels()
  {
    if (_prevShortVideoMode == _shortVideoMode)
    {
      return;
    }

    if (_shortVideoMode)
    {
      _diagnosticsPanel.transform.position = DiagnosticsPanelPosition;
      _effectHistoryPanel.transform.position = EffectsPanelPosition;
      _sceneDiagnosticsPanel.transform.position = ScenePanelPosition;
    }
    else
    {
      _diagnosticsPanel.transform.position = _diagnosticsPanelInitialPosition;
      _effectHistoryPanel.transform.position = _effectHistoryPanelInitialPosition;
      _sceneDiagnosticsPanel.transform.position = _sceneDiagnosticsPanelInitialPosition;
    }
  }

  private void Debug_UpdateDiagnostics()
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

  private void Debug_UpdateEffectHistory()
  {
    _effectHistoryText.Clear();

    var effectHistory = _effects.GetAppliedHistory();
    for (var i = 0; i < effectHistory.Length; i++)
    {
      _effectHistoryText.AppendLine(effectHistory[i].ToEffectString());
    }

    _effectHistoryTextMesh.SetText(_effectHistoryText);
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

  private void DrawDebugTools()
  {
    var sensorFlags = GetSensorFlags();
    _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, sensorFlags, _sensorRayLengths));

    var ceiling = DetectCeiling(sensorFlags, _horizontalDirection);
    var ground = DetectGround(sensorFlags, _horizontalDirection);

    var pos = transform.position;

    // Sensors
    _sensorSystem.Draw();

    // Ceiling normal
    if (ceiling != null)
    {
      var cv = ceiling.Value;
      _meshRenderer.DrawLine(
        cv.Contact,
        cv.Contact + cv.Normal,
        NormalWidth,
        CeilingNormalColor);
    }

    // Ground normal
    if (ground != null)
    {
      var gv = ground.Value;
      _meshRenderer.DrawLine(
        gv.Contact,
        gv.Contact + gv.Normal,
        NormalWidth,
        GroundNormalColor);
    }

    // Speed vector
    _meshRenderer.DrawLine(
      pos,
      pos + (SpeedVectorFactor * new Vector3(_speedSystem.SpeedX, _speedSystem.SpeedY)),
      SpeedVectorWidth,
      SpeedVectorColor);
  }

#if UNITY_EDITOR
  private void UpdateDebug_PauseEditor()
  {
    if (Input.GetKeyDown(KeyCode.KeypadMinus))
    {
      EditorApplication.isPaused = true;
    }
  }
#endif
}
