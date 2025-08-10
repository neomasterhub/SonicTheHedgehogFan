using UnityEngine;

public class SonicController : MonoBehaviour
{
  private Rigidbody2D _rb;
  private Vector2 _velocity;

  public float Acceleration = 10f;
  public float MaxSpeed = 8f;
  public float Friction = 5f;

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

    _rb.linearVelocity = new Vector2(_velocity.x, _rb.linearVelocity.y);
  }
}
