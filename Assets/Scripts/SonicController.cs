using UnityEngine;

public class SonicController : MonoBehaviour
{
  private bool _isGrounded;
  private bool _isTouchingWall;
  private Rigidbody2D _rb;
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
  public float SensorRadius = 0.05f;

  private void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
    _velocity = Vector2.zero;
  }

  private void Update()
  {
    var input = Input.GetAxisRaw(CommonConsts.InputAxis.Horizontal);

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
  }

  private void OnDrawGizmos()
  {
    // Sensor visualization.
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorA, SensorRadius);
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorB, SensorRadius);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorC, SensorRadius);
    Gizmos.DrawWireSphere((Vector2)transform.position + SensorD, SensorRadius);
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
}
