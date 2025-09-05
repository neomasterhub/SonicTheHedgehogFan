using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("UI")]
  public float SensorSourceRadius = 0.05f;

  public Vector2 SensorA => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.AOffset;
  public Vector2 SensorB => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.BOffset;
  public Vector2 SensorC => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.COffset;
  public Vector2 SensorD => (Vector2)transform.position + SonicConsts.SensorOffsets.Default.DOffset;

  private void OnDrawGizmos()
  {
    DrawSensors();
  }

  private void DrawSensors()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(SensorA, SensorSourceRadius);
    Gizmos.DrawSphere(SensorB, SensorSourceRadius);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(SensorC, SensorSourceRadius);
    Gizmos.DrawSphere(SensorD, SensorSourceRadius);
  }
}
