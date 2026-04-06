using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class SonicController : MonoBehaviour
{
  private const float _groundedPositionUpwardOffset = 0.05f;
  private const float _inputDeadZone = 0.001f;
  private const float _skiddingSpeedDeadZone = 0.1f;
  private const float _rotWallToAirDelta = 3;

  private readonly ConditionalValueProvider<float> _slopeFactorSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly GroundInfoSystem _groundInfoSystem;
  private readonly StringBuilder _info;
  private readonly LayerMask _groundLayer;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerSpeedConfig _speedConfig;
  private readonly PlayerSpeedSystem _speedSystem;
  private readonly PlayerViewRotatorProvider<SonicViewRotatorContext> _viewRotatorProvider;
  private readonly RelativeGroundInfo _relativeGroundInfo;
  private readonly SonicSensorSystem _sensorSystem;
  private readonly SonicViewSystem _viewSystem;
  private readonly TimerSystem _timerSystem;

  private bool _isGrounded;
  private bool _prevIsGrounded;
  private bool _postWallDetachInputLock;
  private Animator _animator;
  private GroundSide _prevGroundSide;
  private GroundDetectionResult _lastGroundDetectionResult;
  private PlayerSpeedContext _speedContext;
  private SonicSizeMode _sizeMode;
  private SonicState _state;
  private SonicState _prevState;
  private SonicViewContext _viewContext;
  private SpriteRenderer _spriteRenderer;

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);
  [Header("Physics")]
  public bool GravityEnabled = true;
  public Vector2 WallToAirSpeedDelta = new(0.011f, 0.0f);
  public Vector2 WallDetachPositionOffset = new(-0.1f, 0.0f);
  [Header("Rotators")]
  public bool GroundedViewRotatorEnabled = true;
  public bool WallToAirViewRotatorEnabled = true;
  [Header("Canvas")]
  public TextMeshProUGUI InfoText;
}
