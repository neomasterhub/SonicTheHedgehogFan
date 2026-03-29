using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.InputAxis;
using static SonicConsts.Physics;

[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private const float _groundedPositionUpwardOffset = 0.05f;
  private const float _inputDeadZone = 0.001f;
  private const float _skiddingSpeedDeadZone = 0.1f;

  private readonly ConditionalValueProvider<float> _slopeFactorSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly LayerMask _groundLayer;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerSpeedConfig _speedConfig;
  private readonly PlayerSpeedSystem _speedSystem;
  private readonly RelativeGroundInfo _relativeGroundInfo;
  private readonly SonicSensorSystem _sensorSystem;
  private readonly TimerSystem _timerSystem;

  private bool _isGrounded;
  private bool _prevIsGrounded;
  private bool _postWallDetachInputLock;
  private float _groundAngleDeg;
  private float _groundAngleRad;
  private GroundSide _groundSide;
  private GroundSide _prevGroundSide;
  private GroundDetectionResult _lastGroundDetectionResult;
  private SonicSizeMode _sizeMode;
  private SonicState _state;
  private SonicState _prevState;
  private SpriteRenderer _spriteRenderer;

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);
  [Header("Physics")]
  public bool GravityEnabled = true;
  public Vector2 WallToAirSpeedDelta = new(0.011f, 0.0f);
  public Vector2 WallDetachPositionOffset = new(-0.1f, 0.0f);
}
