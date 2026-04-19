/// <summary>
/// Diagnostics.
/// </summary>
public partial class SonicController
{
  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }

  public void Output()
  {
    _info.Clear();

    _info.AppendLine($"LOC {(_isGrounded ? "Ground" : "Air")}");
    _info.AppendLine($"INS {GetInputState()}");
    _info.AppendLine($"INH {_inputSystem.GetPressedHistory()}");
    _info.AppendLine($"GP {_groundInfoSystem.Previous}");
    _info.AppendLine($"GC {_groundInfoSystem.Current}");
    _info.AppendLine($"GS {_speedSystem.SlopeFactorSpeed * 10000,5:0;-0;0} {_speedSystem.GroundSpeed * 10000,5:0;-0;0}");
    _info.AppendLine($"SP {_speedSystem.SpeedX * 10000,5:0;-0;0} {_speedSystem.SpeedY * 10000,5:0;-0;0}");
    _info.AppendLine($"WL {GetWallInfo(_leftWallDetectionResult)}");
    _info.AppendLine($"WR {GetWallInfo(_rightWallDetectionResult)}");
    _info.AppendLine($"RT {_viewSystem.Rotator}");

    _infoText.SetText(_info);
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
