using UnityEngine;

public class SonicController : MonoBehaviour
{
  private bool _isGrounded;
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
  public float GroundCheckDistance = 0.6f;
  public LayerMask GroundLayer;

  private void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
    _velocity = Vector2.zero;
  }

  private void Update()
  {
    var input = Input.GetAxisRaw(CommonConsts.InputAxis.Horizontal);

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

    _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayer);

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
}
