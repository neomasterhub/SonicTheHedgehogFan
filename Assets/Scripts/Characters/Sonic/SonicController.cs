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
  private readonly SonicSensorSystem _sensorSystem;
  private readonly SonicViewSystem _viewSystem;
  private readonly TimerSystem _timerSystem;

  private bool _isGrounded;
  private bool _prevIsGrounded;
  private bool _postWallDetachInputLock;
  private Animator _animator;
  private GroundDetectionResult _lastGroundDetectionResult;
  private PlayerSpeedContext _speedContext;
  private SonicSizeMode _sizeMode;
  private SonicState _state;
  private SonicState _prevState;
  private SonicViewContext _viewContext;
  private SpriteRenderer _spriteRenderer;

  public bool GravityEnabled = true;
  public bool GroundedViewRotatorEnabled = true;
  public bool WallToAirViewRotatorEnabled = true;
  public TextMeshProUGUI InfoText;
}
