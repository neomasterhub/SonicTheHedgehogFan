using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.InputAxis;
using static SonicConsts.Physics;

/// <summary>
/// Initializations.
/// </summary>
public partial class SonicController
{
  public SonicController()
  {
    _groundLayer = 8;

    _gravitySpeedProvider = new();
    _groundToAirSpeedProvider = new();
    _slopeFactorSpeedProvider = new();

    _relativeGroundInfo = new();
    _sensorSystem = new();
    _timerSystem = new();

    _inputSystem = new(() => Input.GetAxis(Horizontal), () => Input.GetAxis(Vertical));
    _speedConfig = new(TopSpeed, FrictionSpeed, AccelerationSpeed, DecelerationSpeed, AirTopSpeed, AirAccelerationSpeed, MaxFallSpeed, _inputDeadZone, _skiddingSpeedDeadZone);
    _speedSystem = new(_inputSystem, _speedConfig, _gravitySpeedProvider, _slopeFactorSpeedProvider, _groundToAirSpeedProvider);
  }

  private void Awake()
  {
    InitializeEngine();
    InitializeComponents();
    InitializeSpeedSystemProviders();
  }

  private void InitializeEngine()
  {
    Application.targetFrameRate = FramePerSec;
    Time.fixedDeltaTime = 1f / FramePerSec;
    QualitySettings.vSyncCount = 0;
  }

  private void InitializeComponents()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void InitializeSpeedSystemProviders()
  {
    var defaultGravitySpeed = new GravitySpeed(0, 0);
    var gravitySpeed = new GravitySpeed(GravityUpSpeed, GravityDownSpeed);

    _gravitySpeedProvider
      .When(() => GravityEnabled && _groundSide == GroundSide.Down, () => gravitySpeed);

    _slopeFactorSpeedProvider
      .When(() => _groundSide == GroundSide.Down, () => SlopeFactor * Mathf.Sin(_relativeGroundInfo.AngleRad))
      .When(() => _groundSide == GroundSide.Left, () => _relativeGroundInfo.AngleDeg <= 0 ? SlopeFactor : SlopeFactor * Mathf.Cos(_relativeGroundInfo.AngleRad))
      .When(() => _groundSide == GroundSide.Right, () => _relativeGroundInfo.AngleDeg >= 0 ? SlopeFactor : SlopeFactor * Mathf.Cos(_relativeGroundInfo.AngleRad));

    _groundToAirSpeedProvider
      .When(() => _prevGroundSide == GroundSide.Left, () => WallToAirSpeedDelta + new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX))
      .When(() => _prevGroundSide == GroundSide.Right, () => WallToAirSpeedDelta + new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX));

    _gravitySpeedProvider.DefaultProvider = () => defaultGravitySpeed;
    _groundToAirSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }
}
