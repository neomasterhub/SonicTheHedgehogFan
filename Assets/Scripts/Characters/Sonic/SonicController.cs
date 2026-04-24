using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class SonicController
  : MonoBehaviour,
  ILookVerticalDirectionProvider
{
  private readonly ConditionalValueProvider<float> _slopeSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _airToGroundSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly GroundInfoSystem _groundInfoSystem;
  private readonly StringBuilder _info;
  private readonly LayerMask _groundLayer;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerSpeedConfig _speedConfig;
  private readonly PlayerSpeedSystem _speedSystem;
  private readonly PlayerViewRotatorProvider<SonicViewRotatorContext> _viewRotatorProvider;
  private readonly Pipeline _effects;
  private readonly SonicSensorRayLengths _sensorRayLengths;
  private readonly SonicSensorSystem _sensorSystem;
  private readonly SonicViewSystem _viewSystem;
  private readonly TimerSystem _timerSystem;

  private bool _isGrounded;
  private bool _isBalancing;
  private bool _isCurlingUp;
  private bool _isLookingUp;
  private bool _isRolling;
  private bool _isFallingOffWall;
  private bool _prevIsGrounded;
  private bool _postWallDetachInputLock;
  private bool _postWallDetachPositionOffset;
  private bool _triggeredGroundSensorSide;
  private bool _isDownGrounded;
  private bool _isDownGroundedStatic;
  private bool _isDownGroundedMoving;
  private float _slopeFactor;
  private Animator _animator;
  private GameObject _infoPanel;
  private GroundDetectionResult _lastGroundDetectionResult;
  private WallDetectionResult? _leftWallDetectionResult;
  private WallDetectionResult? _rightWallDetectionResult;
  private PlayerSpeedContext _speedContext;
  private SonicSizeMode _sizeMode;
  private SonicSizeMode _prevSizeMode;
  private SonicState _state;
  private SonicState _prevState;
  private SonicViewContext _viewContext;
  private Sound[] _sounds;
  private SpriteRenderer _spriteRenderer;
  private TextMeshProUGUI _infoText;
  private Timer _inputUnlockTimer;

  public bool GravityEnabled = true;
  public bool GroundedViewRotatorEnabled = true;
  public bool WallToAirViewRotatorEnabled = true;
  public AudioClip SkiddingAudioClip;
  public Canvas Canvas;

  public VerticalDirection LookVerticalDirection
  {
    get
    {
      if (_isCurlingUp)
      {
        return VerticalDirection.Down;
      }

      if (_isLookingUp)
      {
        return VerticalDirection.Up;
      }

      return VerticalDirection.None;
    }
  }
}
