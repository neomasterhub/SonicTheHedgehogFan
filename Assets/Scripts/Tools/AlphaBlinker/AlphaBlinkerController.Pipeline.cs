using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class AlphaBlinkerController
{
  private void FixedUpdate()
  {
    _alphaBlinker.Update(Time.fixedDeltaTime);
  }
}
