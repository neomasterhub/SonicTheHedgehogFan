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

    _diagnosticsText = new();
    _effectHistoryText = new();

    _airToGroundSpeedProvider = new();
    _gravitySpeedProvider = new();
    _groundToAirSpeedProvider = new();
    _slopeSpeedProvider = new();

    _effects = new();
    _groundInfoSystem = new();
    _sensorSystem = new();
    _timerSystem = new();
    _viewRotatorProvider = new();

    _inputSystem = new(GetPlayerInput);
    _sensorRayLengths = new(OLength, TopUDFLengths, BottomUDFLengths);
    _speedConfig = new(TopSpeed, FrictionSpeed, MaxSkiddingSpeed, AccelerationSpeed, DecelerationSpeed, AirTopSpeed, AirAccelerationSpeed, MaxFallSpeed, RollFrictionSpeed, RollDecelerationSpeed);
    _speedSystem = new(_inputSystem, _speedConfig, _slopeSpeedProvider, _airToGroundSpeedProvider, _groundToAirSpeedProvider, _gravitySpeedProvider);
    _viewSystem = new(_inputSystem, _viewRotatorProvider);

    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeEngine();
    InitializeComponents();
    InitializeViewSystem();
    InitializeSpeedSystemProviders();
    InitializeSounds();
    InitializeTimers();
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

    _effectHistoryPanel = Canvas.transform.Find("Effect History Panel").gameObject;
    _effectHistoryTextMesh = _effectHistoryPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    _diagnosticsPanel = Canvas.transform.Find("Diagnostics Panel").gameObject;
    _diagnosticsTextMesh = _diagnosticsPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();

#if UNITY_EDITOR
    _effectHistoryPanel.SetActive(true);
    _diagnosticsPanel.SetActive(true);
#endif
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
  }

  private void InitializeSpeedSystemProviders()
  {
    var gravitySpeed = new GravitySpeed(GravityUpSpeed, GravityDownSpeed);

    _gravitySpeedProvider
      .When(() => GravityEnabled && _groundInfoSystem.Current.Side == GroundSide.Down, () => gravitySpeed);

    _slopeSpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Down, () => _slopeFactor * Mathf.Sin(_groundInfoSystem.Current.AngleRad))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Left, () => _groundInfoSystem.Current.AngleDeg <= 0 ? _slopeFactor : _slopeFactor * Mathf.Cos(_groundInfoSystem.Current.AngleRad))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Right, () => _groundInfoSystem.Current.AngleDeg >= 0 ? _slopeFactor : _slopeFactor * Mathf.Cos(_groundInfoSystem.Current.AngleRad));

    _airToGroundSpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Left, () => new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Right, () => new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX));

    _groundToAirSpeedProvider
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Left, () => _isFallingOffWall ? default : WallToAirSpeedDelta + new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX))
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Right, () => _isFallingOffWall ? default : WallToAirSpeedDelta + new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX));

    _gravitySpeedProvider.DefaultProvider = () => GravitySpeed.Zero;
    _airToGroundSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
    _groundToAirSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }

  private void InitializeSounds()
  {
    var rolling = this.AddComponent<AudioSource>();
    rolling.clip = RollingAudioClip;
    rolling.volume = 0.4f;

    var skidding = this.AddComponent<AudioSource>();
    skidding.clip = SkiddingAudioClip;

    _sounds = new Sound[]
    {
      new(rolling,
        () => _isDownGroundedMoving && _isRolling && !_prevIsRolling,
        () => !_isRolling && !rolling.isPlaying),

      new(skidding,
        () => _speedSystem.IsSkidding && !skidding.isPlaying,
        () => !_speedSystem.IsSkidding && !skidding.isPlaying),
    };
  }

  private void InitializeTimers()
  {
    _inputUnlockTimer = new Timer(InputUnlockTimerSeconds)
      .WhenCompleted(() => _postWallDetachInputLock = false);
  }

  private PlayerInput GetPlayerInput()
  {
    if (_postWallDetachInputLock)
    {
      return PlayerInput.None;
    }

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
