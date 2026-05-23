using System;
using UnityEngine;

public static class Helpers
{
  public static class Math
  {
    public static Vector3 Vector3(float x = 0, float y = 0, float z = 0)
    {
      return new Vector3(x, y, z);
    }
  }

  public static class Sensors
  {
    public static GroundDetectionResult? ABDetectGround(
      UDFSensor a,
      UDFSensor b,
      LayerMask groundLayer,
      bool horizontalDirection,
      Func<bool> checkBalancing = null)
    {
      checkBalancing ??= () => false;

      SensorRay dr1;
      SensorRay dr2;

      if (horizontalDirection)
      {
        dr1 = a.DownRay;
        dr2 = b.DownRay;
      }
      else
      {
        dr1 = b.DownRay;
        dr2 = a.DownRay;
      }

      var dr1Hit = dr1.Cast(groundLayer);
      var dr2Hit = dr2.Cast(groundLayer);

      if (dr1Hit != null && dr2Hit != null)
      {
        if (dr1Hit.Value.distance <= dr2Hit.Value.distance)
        {
          return new(!horizontalDirection, dr1Hit.Value, dr1.Direction);
        }
        else
        {
          return new(horizontalDirection, dr2Hit.Value, dr2.Direction);
        }
      }

      SensorRay ur1;
      SensorRay ur2;

      if (horizontalDirection)
      {
        ur1 = a.UpRay;
        ur2 = b.UpRay;
      }
      else
      {
        ur1 = b.UpRay;
        ur2 = a.UpRay;
      }

      var ur1Hit = ur1.Cast(groundLayer);
      var ur2Hit = ur2.Cast(groundLayer);

      if (ur1Hit != null && ur2Hit != null)
      {
        if (ur1Hit.Value.distance >= ur2Hit.Value.distance)
        {
          return new(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below);
        }
        else
        {
          return new(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below);
        }
      }

      if (ur1Hit != null)
      {
        return new(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below, checkBalancing());
      }

      if (ur2Hit != null)
      {
        return new(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below, checkBalancing());
      }

      if (dr1Hit != null)
      {
        return new(!horizontalDirection, dr1Hit.Value, dr1.Direction, VerticalRelation.Above, checkBalancing());
      }

      if (dr2Hit != null)
      {
        return new(horizontalDirection, dr2Hit.Value, dr2.Direction, VerticalRelation.Above, checkBalancing());
      }

      return null;
    }
  }
}
