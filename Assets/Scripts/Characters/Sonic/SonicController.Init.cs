using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.Input.Axis;
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

    _groundInfoSystem = new();
    _info = new();
    _sensorSystem = new();
    _timerSystem = new();
    _viewRotatorProvider = new();

    _inputSystem = new(() => Input.GetAxis(Horizontal), () => Input.GetAxis(Vertical));
    _speedConfig = new(TopSpeed, FrictionSpeed, MaxSkiddingSpeed, AccelerationSpeed, DecelerationSpeed, AirTopSpeed, AirAccelerationSpeed, MaxFallSpeed);
    _speedSystem = new(_inputSystem, _speedConfig, _gravitySpeedProvider, _slopeFactorSpeedProvider, _groundToAirSpeedProvider);
    _viewSystem = new(_inputSystem, _viewRotatorProvider);
  }

  private void Awake()
  {
    InitializeEngine();
    InitializeComponents();
    InitializeViewSystem();
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
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void InitializeViewSystem()
  {
    _viewSystem.SetComponents(_animator, _spriteRenderer);

    var rotGrounded = new GroundedSonicViewRotator(
      () => GroundedViewRotatorEnabled
      && _isGrounded);

    var rotWallToAir = new WallToAirSonicViewRotator(
      _rotWallToAirDelta,
      () => WallToAirViewRotatorEnabled
      && !_isGrounded
      && _prevIsGrounded
      && _groundInfoSystem.Previous.Side is GroundSide.Left or GroundSide.Right);

    _viewRotatorProvider
      .Add(rotGrounded)
      .Add(rotWallToAir);

    _viewRotatorProvider.Default = rotGrounded;
  }

  private void InitializeSpeedSystemProviders()
  {
    var gravitySpeed = new GravitySpeed(GravityUpSpeed, GravityDownSpeed);

    _gravitySpeedProvider
      .When(() => GravityEnabled && _groundInfoSystem.Current.Side == GroundSide.Down, () => gravitySpeed);

    _slopeFactorSpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Down, () => SlopeFactor * Mathf.Sin(_groundInfoSystem.Current.AngleRad))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Left, () => _groundInfoSystem.Current.AngleDeg <= 0 ? SlopeFactor : SlopeFactor * Mathf.Cos(_groundInfoSystem.Current.AngleRad))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Right, () => _groundInfoSystem.Current.AngleDeg >= 0 ? SlopeFactor : SlopeFactor * Mathf.Cos(_groundInfoSystem.Current.AngleRad));

    _groundToAirSpeedProvider
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Left, () => WallToAirSpeedDelta + new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX))
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Right, () => WallToAirSpeedDelta + new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX));

    _gravitySpeedProvider.DefaultProvider = () => GravitySpeed.Zero;
    _groundToAirSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }
}
