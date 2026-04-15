using UnityEngine;

public readonly struct SonicSensorRayLengths
{
  public readonly float O;
  public readonly Vector3 TopUDF;
  public readonly Vector3 BottomUDF;

  public SonicSensorRayLengths(float o, Vector3 topUDF, Vector3 bottomUDF)
  {
    O = o;
    TopUDF = topUDF;
    BottomUDF = bottomUDF;
  }
}
