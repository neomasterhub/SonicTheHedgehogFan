using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private bool _isGrounded;
  private bool _isTouchingWall;
  private Vector2 _velocity;

  [Header("Ground Settings")]
  public LayerMask GroundLayer;
  public float GroundCheckDistance = 0.1f;

  [Header("Jump & Gravity")]
  public float GravityDown = 40f;
  public float GravityUp = 15f;
  public float JumpSpeed = 10f;
  public float MaxFallSpeed = -30f;

  [Header("Sensors")]
  public float SensorLength = 0.5f;

  [Header("UI")]
  public float SensorSourceRadius = 0.03f;

  public Vector2 SensorA => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.AOffset;
  public Vector2 SensorB => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.BOffset;
  public Vector2 SensorC => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.COffset;
  public Vector2 SensorD => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.DOffset;
  public Vector2 SensorE => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.EOffset;
  public Vector2 SensorF => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.FOffset;

  private void Update()
  {
    _isGrounded = IsGrounded();
    _isTouchingWall = IsTouchingWall();

    var hi = Input.GetAxisRaw(CommonConsts.InputAxis.Horizontal);
    var vi = Input.GetAxisRaw(CommonConsts.InputAxis.Vertical);

    Gravity();
    Move();
  }

  private void OnDrawGizmos()
  {
    DrawSensors();
  }

  private void DrawSensors()
  {
    Gizmos.color = SonicConsts.SensorColors.AColor;
    Gizmos.DrawLine(SensorA, SensorA + (Vector2.down * SensorLength));
    Gizmos.DrawSphere(SensorA, SensorSourceRadius);

    Gizmos.color = SonicConsts.SensorColors.BColor;
    Gizmos.DrawSphere(SensorB, SensorSourceRadius);
    Gizmos.DrawLine(SensorB, SensorB + (Vector2.down * SensorLength));

    Gizmos.color = SonicConsts.SensorColors.CColor;
    Gizmos.DrawSphere(SensorC, SensorSourceRadius);
    Gizmos.DrawLine(SensorC, SensorC + (Vector2.left * SensorLength));

    Gizmos.color = SonicConsts.SensorColors.DColor;
    Gizmos.DrawSphere(SensorD, SensorSourceRadius);
    Gizmos.DrawLine(SensorD, SensorD + (Vector2.right * SensorLength));

    Gizmos.color = SonicConsts.SensorColors.EColor;
    Gizmos.DrawSphere(SensorE, SensorSourceRadius);
    Gizmos.DrawLine(SensorE, SensorE + (Vector2.up * SensorLength));

    Gizmos.color = SonicConsts.SensorColors.FColor;
    Gizmos.DrawSphere(SensorF, SensorSourceRadius);
    Gizmos.DrawLine(SensorF, SensorF + (Vector2.up * SensorLength));
  }

  private bool IsGrounded()
  {
    return Physics2D.Raycast(SensorA, Vector2.down, SensorLength, GroundLayer)
      || Physics2D.Raycast(SensorB, Vector2.down, SensorLength, GroundLayer);
  }

  private bool IsTouchingWall()
  {
    return Physics2D.Raycast(SensorC, Vector2.right, SensorLength, GroundLayer)
      || Physics2D.Raycast(SensorD, Vector2.left, SensorLength, GroundLayer);
  }

  private void Gravity()
  {
    if (!_isGrounded)
    {
      _velocity.y -= GravityDown * Time.deltaTime;

      if (_velocity.y < MaxFallSpeed)
      {
        _velocity.y = MaxFallSpeed;
      }
    }
    else
    {
      _velocity.y = 0;
    }
  }

  private void Move()
  {
    transform.position += (Vector3)(_velocity * Time.deltaTime);
  }
}
