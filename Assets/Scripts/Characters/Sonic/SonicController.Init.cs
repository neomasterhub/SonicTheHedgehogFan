using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static SharedConsts.ConvertValues;
using static SonicConsts.Physics;

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

    _configs = new(_physicsMode);
    _inputSystem = new(GetPlayerInput);
    _sensorRayLengths = new(OLength, TopUDFLengths, BottomUDFLengths);
    _speedSystem = new(_configs, _inputSystem, _slopeSpeedProvider, _airToGroundSpeedProvider, _groundToAirSpeedProvider, _gravitySpeedProvider);
    _viewSystem = new(_configs, _inputSystem, _viewRotatorProvider);

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
#if UNITY_EDITOR
    _showDebugInfo = true;
#endif
  }

  private void InitializeComponents()
  {
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _effectHistoryPanel = _canvas.transform.Find("Effect History Panel").gameObject;
    _effectHistoryTextMesh = _effectHistoryPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    _diagnosticsPanel = _canvas.transform.Find("Diagnostics Panel").gameObject;
    _diagnosticsTextMesh = _diagnosticsPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    InitializeGroundNormal();
  }

  private void InitializeViewSystem()
  {
    _viewSystem.SetComponents(_animator, _spriteRenderer);

    var rotGrounded = new GroundedSonicViewRotator(() =>
      _isGrounded);

    var rotWallToAir = new WallToAirSonicViewRotator(() =>
      !_isGrounded
      && _prevIsGrounded
      && _groundInfoSystem.Previous.Side is GroundSide.Left or GroundSide.Right);

    _viewRotatorProvider
      .Add(rotGrounded)
      .Add(rotWallToAir);
  }

  private void InitializeSpeedSystemProviders()
  {
    _gravitySpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Down, () => _configs.PhysicsModeConfig.GravitySpeed);

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
    var jump = this.AddComponent<AudioSource>();
    jump.clip = _jumpAudioClip;

    var roll = this.AddComponent<AudioSource>();
    roll.clip = _rollAudioClip;
    roll.volume = 0.3f;

    var skid = this.AddComponent<AudioSource>();
    skid.clip = _skidAudioClip;

    _sounds = new Sound[]
    {
      new(jump,
        () => _isGrounded && _isJumping,
        () => !_isJumping && !jump.isPlaying),

      new(roll,
        () => _isDownGroundedMoving && !_isJumping && _isRolling && !_prevIsRolling,
        () => !_isRolling && !roll.isPlaying),

      new(skid,
        () => _speedSystem.IsSkidding && !skid.isPlaying,
        () => !_speedSystem.IsSkidding && !skid.isPlaying),
    };
  }

  private void InitializeTimers()
  {
    _inputUnlockTimer = new Timer(InputUnlockTimerSeconds)
      .WhenCompleted(() => _postWallDetachInputLock = false);
  }

  private void InitializeGroundNormal()
  {
    _groundNormal = this.AddComponent<LineRenderer>();
    _groundNormal.material = new(Shader.Find("Sprites/Default"));
    _groundNormal.startColor = Color.white;
    _groundNormal.endColor = Color.white;
    _groundNormal.startWidth = 0.03f;
    _groundNormal.endWidth = 0.03f;
    _groundNormal.positionCount = 2;
    _groundNormal.sortingOrder = 2;
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
