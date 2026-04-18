using System;
using UnityEngine;

public readonly struct SonicSensorRayLengths : IEquatable<SonicSensorRayLengths>
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

  public static bool operator ==(SonicSensorRayLengths left, SonicSensorRayLengths right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(SonicSensorRayLengths left, SonicSensorRayLengths right)
  {
    return !left.Equals(right);
  }

  public bool Equals(SonicSensorRayLengths other)
  {
    return O == other.O && TopUDF == other.TopUDF && BottomUDF == other.BottomUDF;
  }

  public override bool Equals(object obj)
  {
    return obj is SonicSensorRayLengths other && Equals(other);
  }

  public override int GetHashCode()
  {
    var hashCode = default(HashCode);
    hashCode.Add(O);
    hashCode.Add(TopUDF);
    hashCode.Add(BottomUDF);

    return hashCode.ToHashCode();
  }
}
