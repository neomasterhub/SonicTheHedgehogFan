using UnityEditor;
using UnityEngine;

public class SonicController : MonoBehaviour
{
  private bool _isGrounded;
  private bool _isTouchingWall;
  private Animator _animator;
  private Rigidbody2D _rb;
  private SpriteRenderer _spriteRenderer;
  private Vector2 _velocity;

  [Header("Horizontal Movement")]
  public float Acceleration = 10f;
  public float Friction = 5f;
  public float MaxSpeed = 8f;

  [Header("Jump & Gravity")]
  public float GravityDown = 40f;
  public float GravityUp = 15f;
  public float JumpSpeed = 10f;
  public float MaxFallSpeed = -30f;

  [Header("Ground Settings")]
  public LayerMask GroundLayer;

  [Header("Sensors")]
  public Vector2 SensorA = new(0.2f, -0.5f);
  public Vector2 SensorB = new(-0.2f, -0.5f);
  public Vector2 SensorC = new(0.5f, 0f);
  public Vector2 SensorD = new(-0.5f, 0f);
  public Vector2 SensorALabel = new(0.1f, 0.1f);
  public Vector2 SensorBLabel = new(-0.1f, 0.1f);
  public Vector2 SensorCLabel = new(0.15f, 0);
  public Vector2 SensorDLabel = new(0.15f, 0);
  public float SensorRadius = 0.05f;

  private void Start()
  {
    _animator = GetComponent<Animator>();
    _rb = GetComponent<Rigidbody2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _velocity = Vector2.zero;
  }

  private void Update()
  {
    var input = Input.GetAxisRaw(CommonConsts.InputAxis.Horizontal);

    DirectSprite(input);

    _isTouchingWall = IsSensorTouchingWall(SensorC) || IsSensorTouchingWall(SensorD);

    if ((input > 0 && IsSensorTouchingWall(SensorC))
      || (input < 0 && IsSensorTouchingWall(SensorD)))
    {
      input = 0;
    }

    if (input != 0)
    {
      _velocity.x += input * Acceleration * Time.deltaTime;
      _velocity.x = Mathf.Clamp(_velocity.x, -MaxSpeed, MaxSpeed);
    }
    else
    {
      if (_velocity.x > 0)
      {
        _velocity.x = Mathf.Max(_velocity.x - (Friction * Time.deltaTime), 0);
      }
      else if (_velocity.x < 0)
      {
        _velocity.x = Mathf.Min(_velocity.x + (Friction * Time.deltaTime), 0);
      }
    }

    _isGrounded = IsSensorTouchingGround(SensorA) || IsSensorTouchingGround(SensorB);

    var velocityY = _rb.linearVelocity.y;
    if (_isGrounded)
    {
      if (Input.GetButtonDown(CommonConsts.InputAxis.Jump))
      {
        velocityY = JumpSpeed;
      }
      else
      {
        // To prevent falling through the ground.
        velocityY = Mathf.Max(velocityY, 0);
      }
    }
    else
    {
      if (velocityY > 0)
      {
        if (Input.GetButton(CommonConsts.InputAxis.Jump))
        {
          // Lighter gravity while holding jump.
          velocityY -= GravityUp * Time.deltaTime;
        }
        else
        {
          velocityY -= GravityDown * Time.deltaTime;
        }
      }
      else
      {
        velocityY -= GravityDown * Time.deltaTime;
      }

      velocityY = Mathf.Max(velocityY, MaxFallSpeed);
    }

    _rb.linearVelocity = new Vector2(_velocity.x, velocityY);

    SetAnimatorState(_rb.linearVelocity);
  }

  private void OnDrawGizmos()
  {
    var sensorLabelStyle = new GUIStyle();
    sensorLabelStyle.normal.textColor = Color.yellow;
    sensorLabelStyle.alignment = TextAnchor.MiddleCenter;
    sensorLabelStyle.fontSize = 20;

    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorA, SensorRadius);
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorB, SensorRadius);
    Handles.Label((Vector2)transform.position + SensorA + SensorALabel, "A", sensorLabelStyle);
    Handles.Label((Vector2)transform.position + SensorB + SensorBLabel, "B", sensorLabelStyle);

    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorC, SensorRadius);
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorD, SensorRadius);
    Handles.Label((Vector2)transform.position + SensorC + SensorCLabel, "C", sensorLabelStyle);
    Handles.Label((Vector2)transform.position + SensorD + SensorDLabel, "D", sensorLabelStyle);
  }

  private bool IsSensorTouchingGround(Vector2 sensor)
  {
    var sensorPosition = (Vector2)transform.position + sensor;
    var hit = Physics2D.OverlapCircle(sensorPosition, SensorRadius, GroundLayer);

    return hit != null;
  }

  private bool IsSensorTouchingWall(Vector2 sensor)
  {
    var sensorPosition = (Vector2)transform.position + sensor;
    var hit = Physics2D.OverlapCircle(sensorPosition, SensorRadius, GroundLayer);

    return hit != null;
  }

  private void DirectSprite(float input)
  {
    if (input > 0)
    {
      _spriteRenderer.flipX = false;
    }
    else if (input < 0)
    {
      _spriteRenderer.flipX = true;
    }
  }

  private void SetAnimatorState(Vector2 velocity)
  {
    if (Mathf.Abs(velocity.x) > 0.01f)
    {
      _animator.Play(AnimatorStates.Walking);
      _animator.speed = Mathf.Abs(_velocity.x) / MaxSpeed;
    }
    else
    {
      _animator.Play(AnimatorStates.Idle);
      _animator.speed = 1f;
    }
  }

  private static class AnimatorStates
  {
    public const string Idle = nameof(Idle);
    public const string Bored = nameof(Bored);
    public const string Waiting = nameof(Waiting);
    public const string Walking = nameof(Walking);
  }
}
