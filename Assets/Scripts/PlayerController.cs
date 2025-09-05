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
  public Vector2 SensorE => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.EOffset;
  public Vector2 SensorF => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.FOffset;

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
}
