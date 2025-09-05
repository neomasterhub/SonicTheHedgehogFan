using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("UI")]
  public float SensorSourceRadius = 0.03f;

  [Header("Ground Settings")]
  public LayerMask GroundLayer;

  [Header("Sensors")]
  public float SensorLength = 0.5f;

  public Vector2 SensorA => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.AOffset;
  public Vector2 SensorB => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.BOffset;
  public Vector2 SensorC => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.COffset;
  public Vector2 SensorD => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.DOffset;

  private void Update()
  {
    var isGrounded = IsGrounded();
    var isTouchingWall = IsTouchingWall();
  }

  private void OnDrawGizmos()
  {
    DrawSensors();
  }

  private void DrawSensors()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(SensorA, SensorSourceRadius);
    Gizmos.DrawSphere(SensorB, SensorSourceRadius);
    Gizmos.DrawLine(SensorA, SensorA + (Vector2.down * SensorLength));
    Gizmos.DrawLine(SensorB, SensorB + (Vector2.down * SensorLength));

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(SensorC, SensorSourceRadius);
    Gizmos.DrawSphere(SensorD, SensorSourceRadius);
    Gizmos.DrawLine(SensorC, SensorC + (Vector2.right * SensorLength));
    Gizmos.DrawLine(SensorD, SensorD + (Vector2.left * SensorLength));
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
}
