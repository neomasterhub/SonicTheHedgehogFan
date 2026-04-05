using System;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SonicControllerOld : MonoBehaviour
{
  private readonly PlayerViewRotatorProvider _pvrProvider = new();
  private readonly RelativeGroundInfo _relativeGroundInfo = new();
  private readonly SonicSensorSystem _sensorSystem = new();
  private readonly SpeedProvider<GravitySpeed> _gravitySpeedProvider = new();
  private readonly SpeedProvider<float> _slopeFactorSpeedProvider = new();
  private readonly SpeedProvider<Vector2> _groundToAirSpeedProvider = new();
  private readonly StringBuilder _info = new();
  private readonly TimerSystem _timerManager = new();

  // State flags
  private bool _postDetachFall;
  private bool _postDetachInputLocked;
  private bool _wallDetachPositionOffset;
  private GroundSide _groundSide = GroundSide.Down;
  private GroundSide _prevGroundSide = GroundSide.Down;
  private SonicState _playerState;
  private SonicState _prevPlayerState;
  private SonicSizeMode _sizeMode = SonicSizeMode.Big;

  // Components
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;

  private float _groundAngleDeg;
  private AudioSource _sfxSkidding;
  private PlayerInputSystem _inputInfo;
  private IPlayerViewRotator _pvrGrounded;
  private IPlayerViewRotator _pvrWallToAir;
  private PlayerSpeedManager _playerSpeedManager;
  private PlayerViewManager _playerViewManager;
  private Timer _inputUnlockTimer;

  [Header("Sensors")]
  public float ABCDUpLength = 0.1f;
  public float ABCDDownLength = 0.3f;
  public float ABCDFrontLength = 0.5f;

  public SonicControllerOld()
  {
    // For drawing
    //_sensorSystem.SetCurrentSensorGroup(_sizeMode, _groundSide);
  }

  private void Awake()
  {
    Application.targetFrameRate = SharedConsts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / SharedConsts.ConvertValues.FramePerSec;

    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _inputInfo = new PlayerInputSystem(
      () => Input.GetAxis(SharedConsts.InputAxis.Horizontal),
      () => Input.GetAxis(SharedConsts.InputAxis.Vertical));

    _inputUnlockTimer = new Timer(SonicConsts.Physics.InputUnlockTimerSeconds)
      .WhenCompleted(() => _postDetachInputLocked = false);

    InitSpeed();
    InitView();
    InitAudio();
  }

  [Header("Animations")]
  public float AnimatorParameterSpeedAirborneMin = 0.02f;
  public float AnimatorSpeedWalkingMin = 0.5f;
  public float AnimatorSpeedWalkingFactor = 3.0f;

  [Header("Physics")]
  public bool GravityEnabled = true;
  public float TopSpeed = SonicConsts.Physics.TopSpeed;
  public float FrictionSpeed = SonicConsts.Physics.FrictionSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.DecelerationSpeed;
  public float AirTopSpeed = SonicConsts.Physics.AirTopSpeed;
  public float AirAccelerationSpeed = SonicConsts.Physics.AirAccelerationSpeed;
  public float MaxFallSpeed = SonicConsts.Physics.MaxFallSpeed;
  public float GravityUpSpeed = SonicConsts.Physics.GravityUpSpeed;
  public float GravityDownSpeed = SonicConsts.Physics.GravityDownSpeed;
  public float SlopeFactor = SonicConsts.Physics.SlopeFactor;
  public float StandingStraightGroundSpeedZone = SonicConsts.Physics.FrictionSpeed;
  public float SkiddingSpeedDeadZone = 0.1f;
  public float InputDeadZone = 0.001f;
  public float ABSensorLength = 0.1f;
  public float CDSensorLength = 0.5f;
  public float EFSensorLength = 0.1f;
  public float ReversedABSensorLength = 0.3f;
  public float ReversedCDSensorLength = 0;
  public float ReversedEFSensorLength = 0.3f;
  public Vector2 WallDetachPositionOffset = new(-0.1f, 0.0f);
  public Vector2 WallToAirSpeedDelta = new(0.011f, 0.0f);

  [Header("Ground")]
  public LayerMask GroundLayer = 8;
  public float GroundPositionOffset = 0.05f;

  [Header("Rotators")]
  public bool PVRGroundedEnabled = true;
  public bool PVRWallToAirEnabled = true;
  public float PVRWallToAirDelta = 3f;

  [Header("UI")]
  public Color GroundNormalColor = Color.white;
  public float GroundNormalLength = 1.5f;
  public float SensorSourceRadius = 0.03f;

  [Header("Canvas")]
  public TextMeshProUGUI InfoText;

  [Header("Audio")]
  public AudioClip SkiddingAudioClip;

  private PlayerSpeedInput PlayerSpeedInput => new(
    _playerState,
    _prevPlayerState,

    // Sensor Result
    1,//_playerSensorSystemManager.ABResult.Distance,
    1,//_playerSensorSystemManager.ABResult.AngleRad,

    // Ground
    TopSpeed,
    FrictionSpeed,
    AccelerationSpeed,
    DecelerationSpeed,

    // Air
    AirTopSpeed,
    AirAccelerationSpeed,
    MaxFallSpeed,

    // Dead Zones
    InputDeadZone,
    SkiddingSpeedDeadZone);

  //private PlayerViewInput PlayerViewInput => new(
  //  AnimatorParameterSpeedAirborneMin,
  //  AnimatorSpeedWalkingMin,
  //  AnimatorSpeedWalkingFactor,
  //  _playerSpeedManager.SpeedX,
  //  TopSpeed,
  //  _playerSpeedManager.GroundSpeed,
  //  _groundAngleDeg,
  //  _relativeGroundInfo.AngleDeg,
  //  StandingStraightGroundSpeedZone,
  //  _prevGroundSide,
  //  _playerState,
  //  _prevPlayerState);//_playerSensorSystemManager.ABResult.AppliedSensorId);

  

  private void InitSpeed()
  {
    _gravitySpeedProvider
      .Add(() => GravityEnabled && _groundSide == GroundSide.Down, () => new(GravityUpSpeed, GravityDownSpeed));

    _slopeFactorSpeedProvider
      .Add(() => _groundSide == GroundSide.Down, () => SlopeFactor * MathF.Sin(_relativeGroundInfo.AngleRad))
      .Add(() => _groundSide == GroundSide.Left, () => _relativeGroundInfo.AngleRad <= 0 ? SlopeFactor : SlopeFactor * MathF.Cos(_relativeGroundInfo.AngleRad))
      .Add(() => _groundSide == GroundSide.Right, () => _relativeGroundInfo.AngleRad >= 0 ? SlopeFactor : SlopeFactor * MathF.Cos(_relativeGroundInfo.AngleRad));

    _groundToAirSpeedProvider
      .Add(() => _prevGroundSide == GroundSide.Right, () => WallToAirSpeedDelta + new Vector2(-_playerSpeedManager.SpeedY, _playerSpeedManager.SpeedX))
      .Add(() => _prevGroundSide == GroundSide.Left, () => WallToAirSpeedDelta + new Vector2(_playerSpeedManager.SpeedY, -_playerSpeedManager.SpeedX));

    _gravitySpeedProvider.Default = () => new(0, 0);
    _groundToAirSpeedProvider.Default = () => new(_playerSpeedManager.SpeedX, _playerSpeedManager.SpeedY);

    _playerSpeedManager = new PlayerSpeedManager(
      _inputInfo,
      _gravitySpeedProvider,
      _slopeFactorSpeedProvider,
      _groundToAirSpeedProvider);
  }

  private void InitView()
  {
    _pvrGrounded = new GroundedSonicViewRotator(
      () => PVRGroundedEnabled
      && _playerState.HasFlag(SonicState.Grounded));

    _pvrWallToAir = new WallToAirSonicViewRotator(
      PVRWallToAirDelta,
      () => PVRWallToAirEnabled
      && _playerState.HasFlag(SonicState.Airborne)
      && _prevPlayerState.HasFlag(SonicState.Grounded)
      && _prevGroundSide is GroundSide.Left or GroundSide.Right);

    _pvrProvider
      .Add(_pvrGrounded)
      .Add(_pvrWallToAir);

    _pvrProvider.Default = _pvrGrounded;

    _playerViewManager = new PlayerViewManager(
      _animator,
      _inputInfo,
      _pvrProvider,
      _spriteRenderer);
  }

  private void InitAudio()
  {
    _sfxSkidding = this.AddComponent<AudioSource>();
    _sfxSkidding.clip = SkiddingAudioClip;
  }

  private void FixedUpdate()
  {
    UpdateTools();
    ShiftGroundSide();
    UpdateSensors();
    ApplySensors();
    ProcessEvents();
    ApplyMovement();
    UpdateView();
    UpdatePosition();
    UpdateAudio();
    UpdateInfoText();
  }

  private void OnDrawGizmos()
  {
    _sensorSystem.CurrentSensorGroup.Draw();
  }

  private void UpdateTools()
  {
    _inputInfo.Update(!_postDetachInputLocked);
    _timerManager.Update(Time.fixedDeltaTime);
  }

  private void ShiftGroundSide()
  {
    _prevGroundSide = _groundSide;
    _groundSide = _relativeGroundInfo.Side switch
    {
      GroundSide.Left => _groundSide.GetPrevious(),
      GroundSide.Right => _groundSide.GetNext(),
      _ => _groundSide
    };
  }

  private void ApplySensors()
  {
    UpdateSensors();

    //_playerSensorSystemManager.Update(PlayerSensorSystemInput);
    //_playerSensorSystemManager.ApplyAB();
    //_playerSensorSystemManager.ApplyWallSensors();

    //_relativeGroundInfo.Update(_playerSensorSystemManager.ABResult.AngleDeg);

    //_prevPlayerState = _playerState;
    //_playerState = _playerSensorSystemManager.ABResult.GroundDetected
    //  ? PlayerState.Grounded
    //  : PlayerState.Airborne;
  }

  private void ProcessEvents()
  {
    //ProcessEvents_Grounded();
    //ProcessEvents_Airborne();

    //_playerState = _playerState
    //  .SetFlag(PlayerState.PostDetachFall, _postDetachFall);

    //_groundAngleDeg = _relativeGroundInfo.AngleDeg + _groundSide switch
    //{
    //  GroundSide.Down => 0,
    //  GroundSide.Right => 90,
    //  GroundSide.Up => 180,
    //  GroundSide.Left => -90,
    //  _ => throw _groundSide.ArgumentOutOfRangeException()
    //};
  }

  private void ApplyMovement()
  {
    _playerSpeedManager.SetSpeed(PlayerSpeedInput);
    _playerState = _playerState.SetFlag(SonicState.Skidding, _playerSpeedManager.IsSkidding);
  }

  private void UpdateView()
  {
    //_playerViewManager.Update(PlayerViewInput);
  }

  private void UpdatePosition()
  {
    var speedX = _playerSpeedManager.SpeedX;
    var speedY = _playerSpeedManager.SpeedY;

    if (_playerState.HasFlag(SonicState.Grounded))
    {
      if (_wallDetachPositionOffset)
      {
        _wallDetachPositionOffset = false;
        speedX = WallDetachPositionOffset.x;
        speedY = WallDetachPositionOffset.y;
      }

      // Snap to ground with small upward offset.
      // Keeps surface normal aligned with slope.
      //speedY -= (_playerSensorSystemManager.ABResult.Distance
      //  * _playerSensorSystemManager.ABResult.SensorDirectionSign)
      //  - GroundPositionOffset;
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

  private void UpdateAudio()
  {
    if (_playerState.HasFlag(SonicState.Skidding))
    {
      if (!_sfxSkidding.isPlaying)
      {
        _sfxSkidding.Play();
      }
    }
    else
    {
      if (!_sfxSkidding.isPlaying)
      {
        _sfxSkidding.Stop();
      }
    }
  }

  private void UpdateInfoText()
  {
    _info.Clear();

    _info.AddParLine("Prev State", _prevPlayerState);
    _info.AddParLine("Curr State", _playerState);
    _info.AddParLine("Rotator", _pvrProvider.Triggered);
    _info.AddParLine(
      "Input",
      _inputInfo.Enabled ? "On" : "Locked",
      comment: $" ({_inputUnlockTimer.RemainingSeconds.Round(2)} s)",
      addComment: _inputUnlockTimer.IsRunning);

    _info.AppendLine();

    _info.AddParLine("Gravity", GravityEnabled);
    _info.AddParLine("Ground Side", _groundSide);
    _info.AddParLine("Ground Side Angle", _relativeGroundInfo.AngleDeg, " °");
    _info.AddParLine("Ground Angle", _groundAngleDeg, " °");
    _info.AddParLine("Slope Factor Speed", _playerSpeedManager.SlopeFactorSpeed, 4);
    _info.AddParLine("Ground Speed", _playerSpeedManager.GroundSpeed, 4);

    _info.AppendLine();

    _info.AddParLine("Speed X", _playerSpeedManager.SpeedX, 4);
    _info.AddParLine("Speed Y", _playerSpeedManager.SpeedY, 4);

    InfoText.SetText(_info);
  }

  private void ProcessEvents_Grounded()
  {
    if (!_playerState.HasFlag(SonicState.Grounded))
    {
      return;
    }

    if (_postDetachFall)
    {
      _postDetachFall = false;

      if (!_inputUnlockTimer.IsRunning)
      {
        _timerManager.StartIfNotRunning(_inputUnlockTimer);
      }

      return;
    }

    //if (_prevPlayerState.HasFlag(PlayerState.Grounded))
    //{
    //  if (_playerSpeedManager.GroundSpeed == 0
    //    && _groundAngleDeg == 0
    //    && _playerSensorSystemManager.IsOnGroundEdge())
    //  {
    //    _playerState = _playerState.SetFlag(PlayerState.Balancing, true);
    //    return;
    //  }

    //  if (Mathf.Abs(_playerSpeedManager.GroundSpeed) < DecelerationSpeed
    //    && (_groundSide != GroundSide.Down || _relativeGroundInfo.RangeId == GroundRangeId.Steep))
    //  {
    //    _postDetachFall = true;
    //    _postDetachInputLocked = true;
    //    _wallDetachPositionOffset = _groundSide is GroundSide.Left or GroundSide.Right;
    //    _playerSpeedManager.ResetSpeeds();

    //    _groundSide = GroundSide.Down;

    //    return;
    //  }
    //}
  }

  private void ProcessEvents_Airborne()
  {
    if (!_playerState.HasFlag(SonicState.Airborne))
    {
      return;
    }

    _groundSide = GroundSide.Down;
  }

  private void UpdateSensors()
  {
    //_sensorSystem.SetCurrentSensorGroup(_sizeMode, _groundSide);

    //_sensorSystem.CurrentSensorGroup.SetParentPosition(transform.position);

    _sensorSystem.CurrentSensorGroup.A.UpRay.Length = ABCDUpLength;
    _sensorSystem.CurrentSensorGroup.B.UpRay.Length = ABCDUpLength;
    _sensorSystem.CurrentSensorGroup.C.UpRay.Length = ABCDUpLength;
    _sensorSystem.CurrentSensorGroup.D.UpRay.Length = ABCDUpLength;

    _sensorSystem.CurrentSensorGroup.A.DownRay.Length = ABCDDownLength;
    _sensorSystem.CurrentSensorGroup.B.DownRay.Length = ABCDDownLength;
    _sensorSystem.CurrentSensorGroup.C.DownRay.Length = ABCDDownLength;
    _sensorSystem.CurrentSensorGroup.D.DownRay.Length = ABCDDownLength;

    _sensorSystem.CurrentSensorGroup.A.FrontRay.Length = ABCDFrontLength;
    _sensorSystem.CurrentSensorGroup.B.FrontRay.Length = ABCDFrontLength;
    _sensorSystem.CurrentSensorGroup.C.FrontRay.Length = ABCDFrontLength;
    _sensorSystem.CurrentSensorGroup.D.FrontRay.Length = ABCDFrontLength;
  }
}
