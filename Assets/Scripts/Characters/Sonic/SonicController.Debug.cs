#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif
using static SharedConsts.Rendering;
using static SonicConsts.Debug;

/// <summary>
/// Debug.
/// </summary>
public partial class SonicController
{
  private void LateUpdate()
  {
    if (_debugMode)
    {
      DrawSensorSystem();
      DrawNormals();
      DrawSpeedVector();
    }
  }

  private void DrawNormals()
  {
    if (_isGrounded)
    {
      _meshRenderer.DrawLine(
        _lastGroundDetectionResult.Contact,
        _lastGroundDetectionResult.Contact + _lastGroundDetectionResult.Normal,
        NormalWidth,
        GroundNormalColor);
    }

    if (_ceilingDetectionResult.HasValue)
    {
      var ceiling = _ceilingDetectionResult.Value;
      _meshRenderer.DrawLine(
        ceiling.Contact,
        ceiling.Contact + ceiling.Normal,
        NormalWidth,
        CeilingNormalColor);
    }
  }

  private void DrawSensorSystem()
  {
    _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, GetSensorFlags(), _sensorRayLengths));
    _sensorSystem.Draw();
  }

  private void DrawSpeedVector()
  {
    var pos = transform.position;
    _meshRenderer.DrawLine(
      pos,
      pos + (SpeedVectorFactor * new Vector3(_speedSystem.SpeedX, _speedSystem.SpeedY)),
      SpeedVectorWidth,
      SpeedVectorColor);
  }

  private void UpdateDebug()
  {
    if (_debugMode != _prevDebugMode)
    {
      UpdateDebug_Toggle();
    }

    if (_debugMode)
    {
      UpdateDebug_Diagnostics();
      UpdateDebug_EffectHistory();
    }

#if UNITY_EDITOR
    UpdateDebug_PauseEditor();
#endif
  }

  private void UpdateDebug_Toggle()
  {
    _diagnosticsPanel.SetActive(_debugMode);
    _effectHistoryPanel.SetActive(_debugMode);
  }

  private void UpdateDebug_Diagnostics()
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

  private void UpdateDebug_EffectHistory()
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
