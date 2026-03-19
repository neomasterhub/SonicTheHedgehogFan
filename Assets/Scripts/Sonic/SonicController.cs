using System;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private readonly PlayerViewRotatorProvider _pvrProvider = new();
  private readonly PlayerSensorSystemManager _playerSensorSystemManager = new();
  private readonly RelativeGroundInfo _relativeGroundInfo = new();
  private readonly SlopeFactorSpeedProvider _slopeFactorSpeedProvider = new(new()
  {
    [GroundSide.Up] = (_, _) => 0,
    [GroundSide.Down] = (factor, groundAngleRad) => factor * MathF.Sin(groundAngleRad),
    [GroundSide.Right] = (factor, groundAngleRad) => groundAngleRad >= 0 ? factor : factor * MathF.Cos(groundAngleRad),
    [GroundSide.Left] = (factor, groundAngleRad) => groundAngleRad <= 0 ? factor : factor * MathF.Cos(groundAngleRad),
  });
  private readonly StringBuilder _info = new();
  private readonly TimerManager _timerManager = new();

  // Components
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;

  // State flags
  private GroundSide _groundSide = GroundSide.Down;
  private GroundSide _prevGroundSide = GroundSide.Down;
  private PlayerState _playerState;
  private PlayerState _prevPlayerState;
  private SizeMode _playerSizeMode = SizeMode.Big;
  private bool _postDetachFall;
  private bool _postDetachInputLocked;
  private bool _wallDetachPositionOffset;

  // Audio
  private AudioSource _sfxSkidding;
  private Timer _sfxSkiddingTimer;

  private InputInfo _inputInfo;
  private IPlayerViewRotator _prvGrounded;
  private PlayerSpeedManager _playerSpeedManager;
  private PlayerViewManager _playerViewManager;
  private Timer _inputUnlockTimer;
  private float _groundAngleDeg;

  [Header("Animations")]
  public float MinAnimatorWalkingSpeed = 0.5f;
  public float AnimatorWalkingSpeedFactor = 3.0f;
  public float SkiddingSpeedDeadZone = 0.1f;

  [Header("Physics")]
  public bool GravityEnabled = true;
  public float TopSpeed = SonicConsts.Physics.TopSpeed;
  public float FrictionSpeed = SonicConsts.Physics.FrictionSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.DecelerationSpeed;
  public float AirTopSpeed = SonicConsts.Physics.AirTopSpeed;
  public float AirAccelerationSpeed = SonicConsts.Physics.AirAccelerationSpeed;
  public float MaxFallSpeed = SonicConsts.Physics.MaxFallSpeed;
  public float GravityUpSpeed = SonicConsts.Physics.GravityUp;
  public float GravityDownSpeed = SonicConsts.Physics.GravityDown;
  public float SlopeFactor = SonicConsts.Physics.SlopeFactor;
  public float ABSensorLength = 0.1f;
  public float CDSensorLength = 0.1f;
  public float EFSensorLength = 0.1f;
  public float ReversedABSensorLength = 0.3f;
  public float ReversedCDSensorLength = 0.3f;
  public float ReversedEFSensorLength = 0.3f;
  public float InputDeadZone = 0.001f;
  public Vector2 WallDetachPositionOffset = new(-0.1f, 0.0f);
  public Vector2 WallToAirSpeedDelta = new(0.011f, 0.0f);

  [Header("Ground")]
  public LayerMask GroundLayer = 8;

  /// <summary>
  /// Keeps surface normal aligned with slope.
  /// </summary>
  public float GroundPositionOffset = 0.05f; // ABSensorLength / 2

  [Header("UI")]
  public Color GroundNormalColor = Color.white;
  public float GroundNormalLength = 1.5f;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  [Header("Rotators")]
  public bool PRVGroundedEnabled = true;

  [Header("Canvas")]
  public TextMeshProUGUI InfoText;

  private PlayerSensorSystemInput PlayerSensorSystemInput => new(
    transform.position,
    _playerSizeMode,
    _groundSide,
    GroundLayer,
    ABSensorLength,
    CDSensorLength,
    EFSensorLength,
    ReversedABSensorLength,
    ReversedCDSensorLength,
    ReversedEFSensorLength,
    !_spriteRenderer.flipX);

  private PlayerSpeedInput PlayerSpeedInput => new(
    _playerState,
    _prevPlayerState,

    // Sensor Result
    _playerSensorSystemManager.ABResult.Distance,
    _playerSensorSystemManager.ABResult.AngleRad,

    // Ground
    TopSpeed,
    FrictionSpeed,
    AccelerationSpeed,
    DecelerationSpeed,
    SlopeFactor,
    _groundSide,
    _prevGroundSide,

    // Air
    GravityEnabled && _groundSide == GroundSide.Down,
    AirTopSpeed,
    AirAccelerationSpeed,
    GravityUpSpeed,
    GravityDownSpeed,
    MaxFallSpeed,
    WallToAirSpeedDelta,

    // Dead Zones
    InputDeadZone,
    SkiddingSpeedDeadZone);

  private PlayerViewInput PlayerViewInput => new(
    _playerSpeedManager.IsSkidding,
    TopSpeed,
    MinAnimatorWalkingSpeed,
    AnimatorWalkingSpeedFactor,
    _relativeGroundInfo.AngleDeg,
    _groundAngleDeg,
    _playerState,
    _prevPlayerState);

  private void Awake()
  {
    Application.targetFrameRate = Consts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / Consts.ConvertValues.FramePerSec;

    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _inputInfo = new InputInfo(
      () => Input.GetAxis(Consts.InputAxis.Horizontal),
      () => Input.GetAxis(Consts.InputAxis.Vertical));

    _inputUnlockTimer = new Timer(SonicConsts.Times.PostDetachInputUnlockTimerSeconds)
      .WhenCompleted(() => _postDetachInputLocked = false);

    _playerSpeedManager = new PlayerSpeedManager(_inputInfo, _slopeFactorSpeedProvider);

    _prvGrounded = new GroundedPlayerViewRotator(() => PRVGroundedEnabled && _groundSide == GroundSide.Down);
    _pvrProvider
      .Add(_prvGrounded);

    _playerViewManager = new PlayerViewManager(
      _animator,
      _inputInfo,
      _playerSpeedManager,
      _pvrProvider,
      _spriteRenderer);

    InitAudio();
  }

  private void FixedUpdate()
  {
    UpdateInput();
    SetGroundSide();
    RunSensors();
    UpdateState();
    SetSpeed();
    UpdateView();
    UpdatePosition();
    UpdateAudio();
    UpdateInfoText();
  }

  private void OnDrawGizmos()
  {
    _playerSensorSystemManager.DrawGroundNormal(GroundNormalLength, SensorBeginRadius, SensorEndRadius, GroundNormalColor);
    _playerSensorSystemManager.DrawSensors(SensorBeginRadius, SensorEndRadius);
  }

  private void UpdateInput()
  {
    _inputInfo.Update(!_postDetachInputLocked);
  }

  private void SetGroundSide()
  {
    _prevGroundSide = _groundSide;
    _groundSide = _relativeGroundInfo.Side switch
    {
      GroundSide.Left => _groundSide.GetPrevious(),
      GroundSide.Right => _groundSide.GetNext(),
      _ => _groundSide
    };
  }

  private void RunSensors()
  {
    _playerSensorSystemManager.Update(PlayerSensorSystemInput);
    _playerSensorSystemManager.ApplyAB(PlayerSensorSystemInput);
    _relativeGroundInfo.Update(_playerSensorSystemManager.ABResult.AngleDeg);
  }

  private void UpdateState()
  {
    _prevPlayerState = _playerState;
    _playerState = _playerSensorSystemManager.ABResult.GroundDetected
      ? PlayerState.Grounded
      : PlayerState.Airborne;

    if (_playerState.HasFlag(PlayerState.Grounded))
    {
      if (_postDetachFall)
      {
        _postDetachFall = false;
        if (!_inputUnlockTimer.IsRunning)
        {
          _timerManager.RunSingle(_inputUnlockTimer);
        }
      }
      else
      {
        if (Mathf.Abs(_playerSpeedManager.GroundSpeed) < DecelerationSpeed
          && (_groundSide != GroundSide.Down || _relativeGroundInfo.RangeId == GroundRangeId.Steep))
        {
          _postDetachFall = true;
          _postDetachInputLocked = true;
          _wallDetachPositionOffset = _groundSide is GroundSide.Left or GroundSide.Right;
          _playerSpeedManager.ResetSpeeds();

          _groundSide = GroundSide.Down;
        }
      }
    }

    if (_playerState.HasFlag(PlayerState.Airborne))
    {
      _groundSide = GroundSide.Down;
    }

    _groundAngleDeg = _relativeGroundInfo.AngleDeg + _groundSide switch
    {
      GroundSide.Down => 0f,
      GroundSide.Right => 90f,
      GroundSide.Up => 180f,
      GroundSide.Left => -90f,
      _ => throw _groundSide.ArgumentOutOfRangeException()
    };
  }

  private void SetSpeed()
  {
    _playerSpeedManager.SetSpeed(PlayerSpeedInput);
  }

  private void UpdateView()
  {
    _playerViewManager.Update(PlayerViewInput);
  }

  private void UpdatePosition()
  {
    var speedX = _playerSpeedManager.SpeedX;
    var speedY = _playerSpeedManager.SpeedY;

    if (_playerState.HasFlag(PlayerState.Grounded))
    {
      if (_wallDetachPositionOffset)
      {
        _wallDetachPositionOffset = false;
        speedX = WallDetachPositionOffset.x;
        speedY = WallDetachPositionOffset.y;
      }

      // Snap to ground with small upward offset.
      speedY -= (_playerSensorSystemManager.ABResult.Distance
        * _playerSensorSystemManager.ABResult.SensorDirectionSign)
        - GroundPositionOffset;
    }

    // Speed X, Y - offsets in units per frame.
    var pos = transform.position + _groundSide switch
    {
      GroundSide.Down => new Vector3(speedX, speedY),
      GroundSide.Right => new Vector3(-speedY, speedX),
      GroundSide.Up => new Vector3(-speedX, -speedY),
      GroundSide.Left => new Vector3(speedY, -speedX),
      _ => throw _groundSide.ArgumentOutOfRangeException(),
    };

    transform.position = new Vector3(pos.x.Round(2), pos.y.Round(2), transform.position.z);
  }

  private void InitAudio()
  {
    var states = (_animator.runtimeAnimatorController as AnimatorController)
      .layers[0].stateMachine.states;

    var skidding = states
      .Single(s => s.state.name == Consts.Animator.States.Skidding);

    var skiddingToWalking = skidding.state.transitions
      .Single(t => t.destinationState.name == Consts.Animator.States.Walking);

    var skiddingClip = _animator.runtimeAnimatorController.animationClips
      .Single(c => c.name == Consts.Animator.States.Skidding);

    _sfxSkidding = this.AddComponent<AudioSource>();
    _sfxSkidding.clip = Resources.Load<AudioClip>("Sonic/Audio/S1_A4");
    _sfxSkiddingTimer = new(Mathf.Max(
      _sfxSkidding.clip.length,
      skiddingToWalking.exitTime * skiddingClip.length));
    _sfxSkiddingTimer
      .WhenStarted(() => _sfxSkidding.Play());
  }

  private void UpdateAudio()
  {
    _timerManager.OnUpdate(Time.fixedDeltaTime);

    if (_playerState.HasFlag(PlayerState.Skidding) && !_sfxSkidding.isPlaying)
    {
      _timerManager.RunSingle(_sfxSkiddingTimer);
    }
  }

  private void UpdateInfoText()
  {
    _info.Clear();

    _info.AddParLine("Prev State", _prevPlayerState);
    _info.AddParLine("Curr State", _playerState);
    _info.AddParLine(
      "Input",
      _inputInfo.Enabled ? "On" : "Locked",
      comment: $" ({_inputUnlockTimer.RemainingSeconds.Round(2)} s)",
      addComment: _inputUnlockTimer.IsRunning);

    _info.AppendLine();

    _info.AddParLine("Ground Side", _groundSide);
    _info.AddParLine("Ground Side Angle", _relativeGroundInfo.AngleDeg, 0, " °");
    _info.AddParLine("Ground Angle", _groundAngleDeg, 0, " °");
    _info.AddParLine("Slope Factor Speed", _playerSpeedManager.SlopeFactorSpeed, 4);
    _info.AddParLine("Ground Speed", _playerSpeedManager.GroundSpeed, 4);

    _info.AppendLine();

    _info.AddParLine("Global Gravity", GravityEnabled);
    _info.AddParLine("Ground Side Gravity", PlayerSpeedInput.GravityEnabled);

    _info.AppendLine();

    _info.AddParLine("Speed X", _playerSpeedManager.SpeedX, 4);
    _info.AddParLine("Speed Y", _playerSpeedManager.SpeedY, 4);

    InfoText.SetText(_info);
  }
}
