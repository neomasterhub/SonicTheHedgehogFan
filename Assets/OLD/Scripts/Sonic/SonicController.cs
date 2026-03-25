using System;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sensorSystem = new();

  // State flags
  private GroundSide _groundSide = GroundSide.Down;
  private GroundSide _prevGroundSide = GroundSide.Down;
  private PlayerState _playerState;
  private PlayerState _prevPlayerState;
  private SonicSizeMode _sizeMode = SonicSizeMode.Big;

  public SonicController()
  {
    // For drawing
    _sensorSystem.SetCurrentSensorGroup(_sizeMode, _groundSide);
  }

  // OLD:

  private readonly PlayerViewRotatorProvider _pvrProvider = new();
  private readonly PlayerSensorSystemManager2<SonicSizeMode> _playerSensorSystemManager = new(
    SonicConsts.Sensors.Defs);
  private readonly RelativeGroundInfo _relativeGroundInfo = new();
  private readonly SpeedProvider<GravitySpeed> _gravitySpeedProvider = new();
  private readonly SpeedProvider<float> _slopeFactorSpeedProvider = new();
  private readonly SpeedProvider<Vector2> _groundToAirSpeedProvider = new();
  private readonly StringBuilder _info = new();
  private readonly TimerManager _timerManager = new();

  private AudioSource _sfxSkidding;
  private InputInfo _inputInfo;
  private IPlayerViewRotator _pvrGrounded;
  private IPlayerViewRotator _pvrWallToAir;
  private PlayerSpeedManager _playerSpeedManager;
  private PlayerViewManager _playerViewManager;
  private SensorSettings _aSensorSettings;
  private SensorSettings _bSensorSettings;
  private SensorSettings _cSensorSettings;
  private SensorSettings _dSensorSettings;
  private SensorSettings _eSensorSettings;
  private SensorSettings _fSensorSettings;
  private Timer _inputUnlockTimer;
  private float _groundAngleDeg;

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
  public float GravityUpSpeed = SonicConsts.Physics.GravityUp;
  public float GravityDownSpeed = SonicConsts.Physics.GravityDown;
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
  public float GroundPositionOffset = 0.05f; // ABSensorLength / 2

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

  private PlayerSensorSystemInput PlayerSensorSystemInput => new(
    !_spriteRenderer.flipX,
    transform.position,
    _playerSizeMode,
    _groundSide,
    GroundLayer,
    _aSensorSettings.SetLengths(ABSensorLength, ReversedABSensorLength).Enable(true),
    _bSensorSettings.SetLengths(ABSensorLength, ReversedABSensorLength).Enable(true),
    _cSensorSettings.SetLengths(CDSensorLength, ReversedCDSensorLength).Enable(_playerSpeedManager.SpeedX <= 0),
    _dSensorSettings.SetLengths(CDSensorLength, ReversedCDSensorLength).Enable(_playerSpeedManager.SpeedX >= 0),
    _eSensorSettings.SetLengths(EFSensorLength, ReversedEFSensorLength).Enable(false),
    _fSensorSettings.SetLengths(EFSensorLength, ReversedEFSensorLength).Enable(false));

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

    // Air
    AirTopSpeed,
    AirAccelerationSpeed,
    MaxFallSpeed,

    // Dead Zones
    InputDeadZone,
    SkiddingSpeedDeadZone);

  private PlayerViewInput PlayerViewInput => new(
    AnimatorParameterSpeedAirborneMin,
    AnimatorSpeedWalkingMin,
    AnimatorSpeedWalkingFactor,
    _playerSpeedManager.SpeedX,
    TopSpeed,
    _playerSpeedManager.GroundSpeed,
    _groundAngleDeg,
    _relativeGroundInfo.AngleDeg,
    StandingStraightGroundSpeedZone,
    _prevGroundSide,
    _playerState,
    _prevPlayerState,
    _playerSensorSystemManager.ABResult.AppliedSensorId);

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

    InitSensorSettings();
    InitSpeed();
    InitView();
    InitAudio();
  }

  private void InitSensorSettings()
  {
    _aSensorSettings = new SensorSettings(
      ABSensorLength,
      ReversedABSensorLength,
      SonicConsts.Sensors.EnabledColors[SensorId.A],
      SonicConsts.Sensors.DisabledColors[SensorId.A]);
    _bSensorSettings = new SensorSettings(
      ABSensorLength,
      ReversedABSensorLength,
      SonicConsts.Sensors.EnabledColors[SensorId.B],
      SonicConsts.Sensors.DisabledColors[SensorId.B]);
    _cSensorSettings = new SensorSettings(
      CDSensorLength,
      ReversedCDSensorLength,
      SonicConsts.Sensors.EnabledColors[SensorId.C],
      SonicConsts.Sensors.DisabledColors[SensorId.C]);
    _dSensorSettings = new SensorSettings(
      CDSensorLength,
      ReversedCDSensorLength,
      SonicConsts.Sensors.EnabledColors[SensorId.D],
      SonicConsts.Sensors.DisabledColors[SensorId.D]);
    _eSensorSettings = new SensorSettings(
      EFSensorLength,
      ReversedEFSensorLength,
      SonicConsts.Sensors.EnabledColors[SensorId.E],
      SonicConsts.Sensors.DisabledColors[SensorId.E]);
    _fSensorSettings = new SensorSettings(
      EFSensorLength,
      ReversedEFSensorLength,
      SonicConsts.Sensors.EnabledColors[SensorId.F],
      SonicConsts.Sensors.DisabledColors[SensorId.F]);
  }

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
    _pvrGrounded = new GroundedPlayerViewRotator(
      () => PVRGroundedEnabled
      && _playerState.HasFlag(PlayerState.Grounded));

    _pvrWallToAir = new WallToAirPlayerViewRotator(
      PVRWallToAirDelta,
      () => PVRWallToAirEnabled
      && _playerState.HasFlag(PlayerState.Airborne)
      && _prevPlayerState.HasFlag(PlayerState.Grounded)
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
    _playerSensorSystemManager.DrawGroundNormal(GroundNormalLength, SensorSourceRadius, GroundNormalColor);
    _playerSensorSystemManager.DrawSensors(SensorSourceRadius);
  }

  private void UpdateTools()
  {
    _inputInfo.Update(!_postDetachInputLocked);
    _timerManager.OnUpdate(Time.fixedDeltaTime);
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
    _playerSensorSystemManager.Update(PlayerSensorSystemInput);
    _playerSensorSystemManager.ApplyAB();
    _playerSensorSystemManager.ApplyWallSensors();

    _relativeGroundInfo.Update(_playerSensorSystemManager.ABResult.AngleDeg);

    _prevPlayerState = _playerState;
    _playerState = _playerSensorSystemManager.ABResult.GroundDetected
      ? PlayerState.Grounded
      : PlayerState.Airborne;
  }

  private void ProcessEvents()
  {
    ProcessEvents_Grounded();
    ProcessEvents_Airborne();

    _playerState = _playerState
      .SetFlag(PlayerState.PostDetachFall, _postDetachFall);

    _groundAngleDeg = _relativeGroundInfo.AngleDeg + _groundSide switch
    {
      GroundSide.Down => 0,
      GroundSide.Right => 90,
      GroundSide.Up => 180,
      GroundSide.Left => -90,
      _ => throw _groundSide.ArgumentOutOfRangeException()
    };
  }

  private void ApplyMovement()
  {
    _playerSpeedManager.SetSpeed(PlayerSpeedInput);
    _playerState = _playerState.SetFlag(PlayerState.Skidding, _playerSpeedManager.IsSkidding);
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
      // Keeps surface normal aligned with slope.
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

  private void UpdateAudio()
  {
    if (_playerState.HasFlag(PlayerState.Skidding))
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
    if (!_playerState.HasFlag(PlayerState.Grounded))
    {
      return;
    }

    if (_postDetachFall)
    {
      _postDetachFall = false;

      if (!_inputUnlockTimer.IsRunning)
      {
        _timerManager.RunSingle(_inputUnlockTimer);
      }

      return;
    }

    if (_prevPlayerState.HasFlag(PlayerState.Grounded))
    {
      if (_playerSpeedManager.GroundSpeed == 0
        && _groundAngleDeg == 0
        && _playerSensorSystemManager.IsOnGroundEdge())
      {
        _playerState = _playerState.SetFlag(PlayerState.Balancing, true);
        return;
      }

      if (Mathf.Abs(_playerSpeedManager.GroundSpeed) < DecelerationSpeed
        && (_groundSide != GroundSide.Down || _relativeGroundInfo.RangeId == GroundRangeId.Steep))
      {
        _postDetachFall = true;
        _postDetachInputLocked = true;
        _wallDetachPositionOffset = _groundSide is GroundSide.Left or GroundSide.Right;
        _playerSpeedManager.ResetSpeeds();

        _groundSide = GroundSide.Down;

        return;
      }
    }
  }

  private void ProcessEvents_Airborne()
  {
    if (!_playerState.HasFlag(PlayerState.Airborne))
    {
      return;
    }

    _groundSide = GroundSide.Down;
  }
}
