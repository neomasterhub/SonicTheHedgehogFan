using UnityEngine;
using static SharedConsts.ConvertValues;
using static SonicConsts.Physics;

/// <summary>
/// Initializations.
/// </summary>
public partial class SonicController
{
  private void Awake()
  {
    Application.targetFrameRate = FramePerSec;
    Time.fixedDeltaTime = 1f / FramePerSec;

    InitializeSpeedSystemProviders();
  }

  private void InitializeSpeedSystemProviders()
  {
    var gravitySpeed = new GravitySpeed(GravityUpSpeed, GravityDownSpeed);
    var defaultGravitySpeed = new GravitySpeed(0, 0);

    _gravitySpeedProvider
      .When(() => GravityEnabled && _groundSide == GroundSide.Down, () => gravitySpeed);

    _slopeFactorSpeedProvider
      .When(() => _groundSide == GroundSide.Down, () => SlopeFactor * Mathf.Sin(_relativeGroundInfo.AngleRad))
      .When(() => _groundSide == GroundSide.Left, () => 0)
      .When(() => _groundSide == GroundSide.Right, () => 0);

    _groundToAirSpeedProvider
      .When(() => _prevGroundSide == GroundSide.Right, () => WallToAirSpeedDelta + new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX))
      .When(() => _prevGroundSide == GroundSide.Left, () => WallToAirSpeedDelta + new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX));

    _gravitySpeedProvider.DefaultProvider = () => defaultGravitySpeed;
    _groundToAirSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }
}
