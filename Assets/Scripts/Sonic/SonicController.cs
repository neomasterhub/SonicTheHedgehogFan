using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private readonly TimerManager _timerManager = new();
  private readonly SonicSensorSystem _sonicSensorSystem = new();
  private readonly GroundInfo _groundInfo = new();

  // Components
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;

  // Managers
  private InputInfo _inputInfo;
  private Timer _inputLockTimer;
  private PlayerSpeedManager _playerSpeedManager;
  private PlayerViewManager _playerViewManager;

  // States
  private GroundSide _groundSide = GroundSide.Down;
  private PlayerState _playerState = PlayerState.Grounded;
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;

  // Audio
  private Timer _sfxSkiddingTimer;
  private AudioSource _sfxSkidding;

  [Header("Animations")]
  public float MinAnimatorWalkingSpeed = 0.5f;
  public float AnimatorWalkingSpeedFactor = 2.0f;
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
  public float InputDeadZone = 0.001f;
  public bool GravityDownEnabled = true;

  [Header("Ground")]
  public LayerMask GroundLayer = 8;

  /// <summary>
  /// Keeps surface normal aligned with slope.
  /// </summary>
  public float GroundPositionOffset = SonicConsts.Sensors.Length / 2;

  [Header("UI")]
  public float GroundNormalLength = 1.5f;
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private PlayerSpeedInput PlayerSpeedInput => new()
  {
    DistanceToGround = _sonicSensorSystem.ABResult.Distance,
    GroundAngleRad = _sonicSensorSystem.ABResult.AngleRad,
    GroundSensorLength = SensorLength,
    TopSpeed = TopSpeed,
    FrictionSpeed = FrictionSpeed,
    AccelerationSpeed = AccelerationSpeed,
    DecelerationSpeed = DecelerationSpeed,
    AirTopSpeed = AirTopSpeed,
    AirAccelerationSpeed = AirAccelerationSpeed,
    MaxFallSpeed = MaxFallSpeed,
    GravityUpSpeed = GravityUpSpeed,
    GravityDownSpeed = GravityDownSpeed,
    InputDeadZone = InputDeadZone,
    GravityDownEnabled = GravityDownEnabled,
    SlopeFactor = SlopeFactor,
  };

  private PlayerViewInput PlayerViewInput => new()
  {
    TopSpeed = TopSpeed,
    MinAnimatorWalkingSpeed = MinAnimatorWalkingSpeed,
    AnimatorWalkingSpeedFactor = AnimatorWalkingSpeedFactor,
  };

  private void Awake()
  {
    Application.targetFrameRate = Consts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / Consts.ConvertValues.FramePerSec;

    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _inputInfo = new InputInfo(
      () => Input.GetAxis(Consts.InputAxis.Horizontal),
      () => Input.GetAxis(Consts.InputAxis.Vertical));

    _inputLockTimer = new Timer(SonicConsts.Times.InputLockSeconds)
      .WhenStarted(() =>
      {
        _inputInfo.Enabled = false;
        _playerSpeedManager.ResetGroundSpeed();
      })
      .WhenCompleted(() => _inputInfo.Enabled = true);

    _playerSpeedManager = new PlayerSpeedManager(_inputInfo);

    _playerViewManager = new PlayerViewManager(
      _animator,
      _inputInfo,
      _playerSpeedManager,
      _spriteRenderer);

    InitAudio();
  }

  private void FixedUpdate()
  {
    UpdateInput();
    RunSensors();
    UpdateState();
    EnableInput();
    SetSpeed();
    UpdateView();
    UpdatePosition();
    UpdateAudio();
  }

  private void OnDrawGizmos()
  {
    _sonicSensorSystem.DrawSensors(SensorBeginRadius, SensorEndRadius);
    _sonicSensorSystem.DrawGroundNormal(GroundNormalLength, SensorBeginRadius, SensorEndRadius);
  }

  private void UpdateInput()
  {
    _inputInfo.Update();
  }

  private void RunSensors()
  {
    _sonicSensorSystem.Update(transform.position, _sonicSizeMode, _groundSide, SensorLength);
    _sonicSensorSystem.ApplyAB(GroundLayer, SensorLength);
  }

  private void UpdateState()
  {
    var playerState = PlayerState.None;

    playerState |= _sonicSensorSystem.ABResult.GroundDetected
      ? PlayerState.Grounded
      : PlayerState.Airborne;

    if (playerState.HasFlag(PlayerState.Grounded))
    {
      if ((_inputInfo.X > InputDeadZone
        && _playerSpeedManager.SpeedX < -SkiddingSpeedDeadZone)
        || (_inputInfo.X < -InputDeadZone
        && _playerSpeedManager.SpeedX > SkiddingSpeedDeadZone))
      {
        playerState |= PlayerState.Skidding;
      }

      _groundInfo.Update(_sonicSensorSystem.ABResult.AngleDeg);
      if (_groundInfo.RangeId == GroundRangeId.Steep
        && Mathf.Abs(_playerSpeedManager.GroundSpeed) < DecelerationSpeed)
      {
        playerState |= PlayerState.LockedInput;
      }
    }

    if (playerState == PlayerState.Airborne)
    {
    }

    _playerState = playerState;
  }

  private void EnableInput()
  {
    if (_playerState.HasFlag(PlayerState.LockedInput) && !_inputLockTimer.IsRunning)
    {
      _timerManager.RunSingle(_inputLockTimer);
    }
  }

  public void SetSpeed()
  {
    _playerSpeedManager.SetSpeed(_playerState, PlayerSpeedInput);
  }

  public void UpdateView()
  {
    _playerViewManager.Update(_playerState, PlayerViewInput);
  }

  private void UpdatePosition()
  {
    // Snap to ground with small upward offset.
    var speedY = _playerSpeedManager.SpeedY;
    if (_playerState.HasFlag(PlayerState.Grounded))
    {
      speedY -= (_sonicSensorSystem.ABResult.Distance
        * _sonicSensorSystem.ABResult.SensorDirectionSign)
        - GroundPositionOffset;
    }

    // Speed X, Y - offsets in units per frame.
    transform.position += new Vector3(_playerSpeedManager.SpeedX, speedY);
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
}
