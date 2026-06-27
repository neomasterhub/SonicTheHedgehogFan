using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class SonicController : MonoBehaviour
{
  private readonly ConditionalValueProvider<float> _slopeSpeedProvider;
  private readonly ConditionalValueProvider<float> _gravitySpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _airToGroundSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly GroundInfoSystem _groundInfoSystem;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerViewRotatorProvider<SonicViewRotatorContext> _viewRotatorProvider;
  private readonly Pipeline _effects;
  private readonly SonicConfigs _configs;
  private readonly SonicSensorRayLengths _sensorRayLengths;
  private readonly SonicSensorSystem _sensorSystem;
  private readonly SonicSpeedSystem _speedSystem;
  private readonly SonicViewSystem _viewSystem;
  private readonly StringBuilder _diagnosticsText;
  private readonly StringBuilder _effectHistoryText;
  private readonly TimerSystem _timerSystem;

  private bool _isGrounded;
  private bool _isBalancing;
  private bool _isCurlingUp;
  private bool _isLookingUp;
  private bool _isRolling;
  private bool _isFallingOffWall;
  private bool _isJumping;
  private bool _isDying;
  private bool _isDead;
  private bool _isRollingJumped;
  private bool _isStoppedByCeiling;
  private bool _prevIsGrounded;
  private bool _prevIsRolling;
  private bool _postWallDetachDpadLock;
  private bool _postWallDetachPositionOffset;
  private bool _ringCollected;
  private bool _ringsLost;
  private bool _isDownGrounded;
  private bool _isDownGroundedStatic;
  private bool _isDownGroundedMoving;
  private bool _isUpGrounded;
  private bool _isLeftGrounded;
  private bool _isRightGrounded;
  private bool _debugMode;
  private bool _prevDebugMode;
  private bool _takeLeftHit;
  private bool _takeRightHit;
  private bool _horizontalDirection;
  private bool _hasShield;
  private bool _prevHasShield;
  private char _triggeredGroundSensorId;
  private float _slopeFactor;
  private float _absGroundSpeed;
  private float? _reboundGroundSpeed;
  private Vector2? _reboundAirSpeed;
  private Animator _animator;
  private BoxCollider2D _boxCollider;
  private CeilingDetectionResult? _ceilingDetectionResult;
  private GameObject _diagnosticsPanel;
  private GameObject _effectHistoryPanel;
  private GameObject _shield;
  private GroundDetectionResult _lastGroundDetectionResult;
  private IPlatform _contactPlatform;
  private LineRenderer _groundNormal;
  private PhysicsMode _physicsMode;
  private PhysicsMode _prevPhysicsMode;
  private ReboundSignal? _reboundSignal;
  private SonicSpeedContext _speedContext;
  private SonicSizeMode _sizeMode;
  private SonicSizeMode _prevSizeMode;
  private SonicViewContext _viewContext;
  private Sound[] _sounds;
  private SpriteRenderer _spriteRenderer;
  private TextMeshProUGUI _diagnosticsTextMesh;
  private TextMeshProUGUI _effectHistoryTextMesh;
  private Timer _dyingTimer;
  private Timer _dpadLockTimer;
  private Timer _postHurtInvincibleTimer;
  private Timer _ringCollectorDisabledTimer;
  private Transform _contactCeilingTransform;
  private Transform _contactGroundTransform;
  private Transform _contactLeftWallTransform;
  private Transform _contactRightWallTransform;
  private WallDetectionResult? _leftWallDetectionResult;
  private WallDetectionResult? _rightWallDetectionResult;

  [SerializeField]
  private Canvas _canvas;

  [Header("Audio")]
  [SerializeField]
  [InspectorLabel("Jump")]
  private AudioClip _jumpAudioClip;
  [SerializeField]
  [InspectorLabel("Roll")]
  private AudioClip _rollAudioClip;
  [SerializeField]
  [InspectorLabel("Skid")]
  private AudioClip _skidAudioClip;
  [SerializeField]
  [InspectorLabel("Ring")]
  private AudioClip _ringAudioClip;
  [SerializeField]
  [InspectorLabel("Lost rings")]
  private AudioClip _lostRingsClip;
  [SerializeField]
  [InspectorLabel("Death")]
  private AudioClip _deathClip;
  [SerializeField]
  [InspectorLabel("Shield")]
  private AudioClip _shieldClip;
  [SerializeField]
  [InspectorLabel("Lost shield")]
  private AudioClip _lostShieldClip;

  [Header("Prefabs")]
  [SerializeField]
  [InspectorLabel("Ring")]
  private GameObject _ringPrefab;
}
