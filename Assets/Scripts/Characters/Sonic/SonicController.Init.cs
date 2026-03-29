using UnityEngine;
using static SharedConsts.ConvertValues;

/// <summary>
/// Initializations.
/// </summary>
public partial class SonicController
{
  private void Awake()
  {
    Application.targetFrameRate = FramePerSec;
    Time.fixedDeltaTime = 1f / FramePerSec;
  }
}
