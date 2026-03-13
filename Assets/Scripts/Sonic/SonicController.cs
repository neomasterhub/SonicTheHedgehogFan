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
  private PlayerState _playerState = PlayerState.Grounded;
  private SizeMode _playerSizeMode = SizeMode.Big;

  // Audio
  private AudioSource _sfxSkidding;
  private Timer _sfxSkiddingTimer;

  private InputInfo _inputInfo;
  private Timer _inputUnlockTimer;
  private PlayerSpeedManager _playerSpeedManager;
  private PlayerViewManager _playerViewManager;

  [Header("Animations")]
  public float MinAnimatorWalkingSpeed = 0.5f;
  public float AnimatorWalkingSpeedFactor = 3.0f;
  public float SkiddingSpeedDeadZone = 0.1f;

  [Header("Physics")]
  public float TopSpeed = SonicConsts.Physics.TopSpeed;
  public float FrictionSpeed = SonicConsts.Physics.FrictionSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.DecelerationSpeed;
  public float AirTopSpeed = SonicConsts.Physics.AirAccelerationSpeed;
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
  public bool GravityDownEnabled = true;

  [Header("Ground")]
  public LayerMask GroundLayer = 8;

  /// <summary>
  /// Keeps surface normal aligned with slope.
  /// </summary>
  public float GroundPositionOffset = 0.05f; // ABSensorLength / 2

  [Header("UI")]
  public float GroundNormalLength = 1.5f;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

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

    // Air
    AirTopSpeed,
    AirAccelerationSpeed,
    GravityUpSpeed,
    GravityDownSpeed,
    MaxFallSpeed,
    GravityDownEnabled,

    // Dead Zones
    InputDeadZone,
    SkiddingSpeedDeadZone);

  private PlayerViewInput PlayerViewInput => new(
    _playerSpeedManager.IsSkidding,
    TopSpeed,
    MinAnimatorWalkingSpeed,
    AnimatorWalkingSpeedFactor,
    _groundSide);

  private void Awake()
  {
    Application.targetFrameRate = Consts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / Consts.ConvertValues.FramePerSec;

    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _inputInfo = new InputInfo(
      () => Input.GetAxis(Consts.InputAxis.Horizontal),
      () => Input.GetAxis(Consts.InputAxis.Vertical));

    _inputUnlockTimer = new Timer(SonicConsts.Times.InputUnlockTimerSeconds)
      .WhenCompleted(() => _inputInfo.Enabled = true);

    _playerSpeedManager = new PlayerSpeedManager(_inputInfo, _slopeFactorSpeedProvider);

    _playerViewManager = new PlayerViewManager(
      _animator,
      _inputInfo,
      _playerSpeedManager,
      _spriteRenderer);

    InitAudio();
  }

  private void FixedUpdate()
  {
    UpdateInfoText();
    UpdateInput();
    SetGroundSide();
    RunSensors();
    UpdateState();
    SetSpeed();
    UpdateView();
    UpdatePosition();
    UpdateAudio();
  }

  private void OnDrawGizmos()
  {
    _playerSensorSystemManager.DrawSensors(SensorBeginRadius, SensorEndRadius);
    _playerSensorSystemManager.DrawGroundNormal(GroundNormalLength, SensorBeginRadius, SensorEndRadius, Color.brown);
  }

  private void UpdateInput()
  {
    _inputInfo.Update();
  }

  private void SetGroundSide()
  {
    _relativeGroundInfo.Update(_playerSensorSystemManager.ABResult.AngleDeg);
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
  }

  private void UpdateState()
  {
    var playerState = PlayerState.None;

    playerState |= _playerSensorSystemManager.ABResult.GroundDetected
      ? PlayerState.Grounded
      : PlayerState.Airborne;

    if (playerState.HasFlag(PlayerState.Grounded))
    {
      if (!_inputInfo.Enabled && !_inputUnlockTimer.IsRunning)
      {
        _timerManager.RunSingle(_inputUnlockTimer);
      }

      if (_inputInfo.Enabled
        && Mathf.Abs(_playerSpeedManager.GroundSpeed) < DecelerationSpeed
        && (_groundSide != GroundSide.Down || _relativeGroundInfo.RangeId == GroundRangeId.Steep))
      {
        _inputInfo.Enabled = false;
        _groundSide = GroundSide.Down;
        _playerSpeedManager.ResetGroundSpeed();
        playerState &= ~PlayerState.Grounded;
        playerState |= PlayerState.Airborne;
      }
    }

    if (playerState.HasFlag(PlayerState.Airborne))
    {
    }

    _playerState = playerState;
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

    // Snap to ground with small upward offset.
    if (_playerState.HasFlag(PlayerState.Grounded))
    {
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

    transform.position = new Vector3(
      MathF.Round(pos.x, 3),
      MathF.Round(pos.y, 3),
      transform.position.z);
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

    _info.AppendFormat("Input: {0}", _inputInfo.Enabled ? "on" : "locked");
    _info.AppendLine(_inputUnlockTimer.IsRunning ? $" ({_inputUnlockTimer.RemainingSeconds.Round(2)} s)" : null);

    _info.AppendLineFormat("Ground Speed: {0}", _playerSpeedManager.GroundSpeed.Round(4));
    _info.AppendLineFormat("Slope Factor Speed: {0}", _playerSpeedManager.SlopeFactorSpeed.Round(4));

    InfoText.SetText(_info);
  }
}
