using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static SonicConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class SonicController
{
  public SonicController()
  {
    _diagnosticsText = new();
    _effectHistoryText = new();

    _airToGroundSpeedProvider = new();
    _gravitySpeedProvider = new();
    _groundToAirSpeedProvider = new();
    _slopeSpeedProvider = new();

    _groundInfoSystem = new();
    _sensorSystem = new();
    _timerSystem = new();
    _viewRotatorProvider = new();

    _configs = new(_physicsMode);
    _inputSystem = new(GetPlayerInput);
    _sensorRayLengths = new(OLength, TopUDFLengths, BottomUDFLengths);
    _speedSystem = new(_configs, _inputSystem, _slopeSpeedProvider, _gravitySpeedProvider, _airToGroundSpeedProvider, _groundToAirSpeedProvider);
    _viewSystem = new(_configs, _inputSystem, _viewRotatorProvider);

    CanCollectRing = true;
    Rings = new Collector()
      .WhenAdded(() => _ringCollected = true);

    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
    InitializeViewSystem();
    InitializeSensorSystem();
    InitializeSpeedSystemProviders();
    InitializeSounds();
    InitializeTimers();
  }

  private void InitializeComponents()
  {
    _animator = GetComponent<Animator>();
    _boxCollider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _effectHistoryPanel = _canvas.transform.Find("Sonic Effect History Panel").gameObject;
    _effectHistoryTextMesh = _effectHistoryPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    _diagnosticsPanel = _canvas.transform.Find("Sonic Diagnostics Panel").gameObject;
    _diagnosticsTextMesh = _diagnosticsPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    _invincibilityStars = transform.Find("Invincibility Stars").gameObject;
    CreateInvincibilityStarsTrailChain();

    _shield = transform.Find("Shield").gameObject;

    InitializeGroundNormal();
  }

  private void InitializeViewSystem()
  {
    _viewSystem.SetComponents(_animator, _spriteRenderer);

    var rotGrounded = new GroundedSonicViewRotator(() =>
      _isGrounded);

    var rotWallToAir = new WallToAirSonicViewRotator(() =>
      !_isGrounded
      && !_isRolling
      && _prevIsGrounded
      && _groundInfoSystem.Previous.Side is GroundSide.Left or GroundSide.Right);

    var rotCeilingToAir = new CeilingToAirSonicViewRotator(() =>
      !_isGrounded
      && !_isRolling
      && _prevIsGrounded
      && _groundInfoSystem.Previous.Side == GroundSide.Up);

    _viewRotatorProvider
      .Add(rotGrounded)
      .Add(rotWallToAir)
      .Add(rotCeilingToAir);
  }

  private void InitializeSensorSystem()
  {
    _sensorSystem.SetMeshRenderer(_meshRendererObj.GetComponent<IMeshRenderer>());
  }

  private void InitializeSpeedSystemProviders()
  {
    _gravitySpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Down, GetDownGroundGravitySpeed);

    _slopeSpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Down, () => _slopeFactor * Mathf.Sin(_groundInfoSystem.Current.AngleRad))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Left, () => _groundInfoSystem.Current.SideAngleDeg <= 0 ? -_slopeFactor : -_slopeFactor * Mathf.Cos(_groundInfoSystem.Current.SideAngleRad))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Right, () => _groundInfoSystem.Current.SideAngleDeg >= 0 ? _slopeFactor : _slopeFactor * Mathf.Cos(_groundInfoSystem.Current.SideAngleRad));

    _airToGroundSpeedProvider
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Left, () => new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX))
      .When(() => _groundInfoSystem.Current.Side == GroundSide.Right, () => new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX));

    _groundToAirSpeedProvider
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Left, () => _isFallingOffWall ? default : -WallToAirSpeedDelta + new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX))
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Right, () => _isFallingOffWall ? default : WallToAirSpeedDelta + new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX))
      .When(() => _groundInfoSystem.Previous.Side == GroundSide.Up, () => new Vector2(-_speedSystem.SpeedX, _isJumping ? Mathf.Min(0, -_speedSystem.SpeedY) : 0));

    _gravitySpeedProvider.DefaultProvider = () => 0;
    _airToGroundSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
    _groundToAirSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }

  private void InitializeSounds()
  {
    _ringAudioSource = this.AddComponent<AudioSource>();
    _ringAudioSource.clip = _ringAudioClip;

    _shieldAudioSource = this.AddComponent<AudioSource>();
    _shieldAudioSource.clip = _shieldAudioClip;

    var death = this.AddComponent<AudioSource>();
    death.clip = _deathAudioClip;

    var jump = this.AddComponent<AudioSource>();
    jump.clip = _jumpAudioClip;

    var lostRings = this.AddComponent<AudioSource>();
    lostRings.clip = _lostRingsAudioClip;

    var lostShield = this.AddComponent<AudioSource>();
    lostShield.clip = _lostShieldAudioClip;

    var roll = this.AddComponent<AudioSource>();
    roll.clip = _rollAudioClip;
    roll.volume = 0.3f;

    var skid = this.AddComponent<AudioSource>();
    skid.clip = _skidAudioClip;

    _sounds = new Sound[]
    {
      new(_ringAudioSource,
        () => !_isGettingRingFromMonitor && _ringCollected,
        () => !_isGettingRingFromMonitor && _ringCollected && _ringAudioSource.isPlaying),

      new(_shieldAudioSource,
        () => !_isGettingShieldFromMonitor && _hasShield && !_prevHasShield),

      new(death,
        () => IsHit && _isDying),

      new(jump,
        () => _isGrounded && _isJumping,
        () => !_isJumping && !jump.isPlaying),

      new(lostRings,
        () => _ringsLost),

      new(lostShield,
        () => !_hasShield && _prevHasShield),

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
    _dpadLockTimer = new Timer(DpadLockDuration)
      .WhenCompleted(() => _postWallDetachDpadLock = false);

    _dyingTimer = new Timer(DyingDuration)
      .WhenCompleted(() =>
      {
        _isDying = false;
        _isDead = true;
        gameObject.SetActive(false);
      });

    _invincibilityStarsTimer = new Timer(InvincibilityStarsDuration)
      .WhenCompleted(() =>
      {
        _hasInvincibilityStars = false;
        _invincibilityStars.SetActive(false);

        IsInvincible = false;
        IsAttacking = _isRolling;
      });

    _postHurtInvincibleTimer = new Timer(PostHurtInvincibleDuration)
      .WhenCompleted(() =>
      {
        if (!_hasInvincibilityStars)
        {
          IsInvincible = false;
        }
      });

    _ringCollectorDisabledTimer = new Timer(RingCollectorDisabledDuration)
      .WhenCompleted(() => CanCollectRing = !_isDying);

    _speedShoesTimer = new Timer(SpeedShoesDuration)
      .WhenCompleted(() => _hasSpeedShoes = false);
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

  private void CreateInvincibilityStarsTrailChain()
  {
    var t1 = Instantiate(_invincibilityStarsTrailPrefab, _invincibilityStars.transform);
    var t2 = Instantiate(_invincibilityStarsTrailPrefab, t1.transform);
    Instantiate(_invincibilityStarsTrailPrefab, t2.transform);
  }

  private float GetDownGroundGravitySpeed()
  {
    if (IsHurt)
    {
      return _configs.PhysicsModeConfig.HurtGravitySpeed;
    }

    if (_isDying)
    {
      return _configs.PhysicsModeConfig.GravitySpeed;
    }

    return _configs.PhysicsModeConfig.GravitySpeed;
  }

  private PlayerInput GetPlayerInput()
  {
    if (_isDying)
    {
      return PlayerInput.None;
    }

    var input = PlayerInput.None
      .Set(PlayerInput.Start, Input.GetKey(KeyCode.KeypadEnter))
      .Set(PlayerInput.A, Input.GetKey(KeyCode.Keypad1))
      .Set(PlayerInput.B, Input.GetKey(KeyCode.Keypad2))
      .Set(PlayerInput.C, Input.GetKey(KeyCode.Keypad3))
      .Set(PlayerInput.X, Input.GetKey(KeyCode.Keypad4))
      .Set(PlayerInput.Y, Input.GetKey(KeyCode.Keypad5))
      .Set(PlayerInput.Z, Input.GetKey(KeyCode.Keypad6));

    if (IsHurt
      || _isRollingJumped
      || _postWallDetachDpadLock)
    {
      return input;
    }

    return input
      .Set(PlayerInput.Up, Input.GetKey(KeyCode.UpArrow))
      .Set(PlayerInput.Down, Input.GetKey(KeyCode.DownArrow))
      .Set(PlayerInput.Left, Input.GetKey(KeyCode.LeftArrow))
      .Set(PlayerInput.Right, Input.GetKey(KeyCode.RightArrow));
  }
}
