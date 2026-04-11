using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static SharedConsts.ConvertValues;
using static SonicConsts.Physics;
using static SonicConsts.View;

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

    _inputSystem = new(GetPlayerInput);
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
    InitializeSounds();
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
    _infoPanel = Canvas.transform.Find("Info Panel").gameObject;
    _infoText = _infoPanel.transform.Find("Info Text").GetComponent<TextMeshProUGUI>();
  }

  private void InitializeViewSystem()
  {
    _viewSystem.SetComponents(_animator, _spriteRenderer);

    var rotGrounded = new GroundedSonicViewRotator(
      () => GroundedViewRotatorEnabled
      && _isGrounded);

    var rotWallToAir = new WallToAirSonicViewRotator(
      WallToAirViewRotatorAngleDegDelta,
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

  private void InitializeSounds()
  {
    var skidding = this.AddComponent<AudioSource>();
    skidding.clip = SkiddingAudioClip;

    _sounds = new Sound[]
    {
      new(skidding, () => _speedSystem.IsSkidding && !skidding.isPlaying, () => !_speedSystem.IsSkidding && !skidding.isPlaying),
    };
  }

  private PlayerInput GetPlayerInput()
  {
    return PlayerInput.None
      .Set(PlayerInput.Start, Input.GetKey(KeyCode.KeypadEnter))
      .Set(PlayerInput.Left, Input.GetKey(KeyCode.LeftArrow))
      .Set(PlayerInput.Right, Input.GetKey(KeyCode.RightArrow))
      .Set(PlayerInput.Up, Input.GetKey(KeyCode.UpArrow))
      .Set(PlayerInput.Down, Input.GetKey(KeyCode.DownArrow))
      .Set(PlayerInput.A, Input.GetKey(KeyCode.Keypad1))
      .Set(PlayerInput.B, Input.GetKey(KeyCode.Keypad2))
      .Set(PlayerInput.C, Input.GetKey(KeyCode.Keypad3))
      .Set(PlayerInput.X, Input.GetKey(KeyCode.Keypad4))
      .Set(PlayerInput.Y, Input.GetKey(KeyCode.Keypad5))
      .Set(PlayerInput.Z, Input.GetKey(KeyCode.Keypad6));
  }
}
