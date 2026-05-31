using System;
using UnityEngine;
using static SharedConsts.Physics;

public static class Helpers
{
  public static class Diagnostics
  {
    private static float _fps;
    private static float _fpsExpiredAt;

    public static float GetFPS(float delay = 1)
    {
      if (Time.unscaledTime > _fpsExpiredAt)
      {
        _fpsExpiredAt = Time.unscaledTime.Round() + delay;
        _fps = (1 / Time.unscaledDeltaTime).Round();
      }

      return _fps;
    }
  }

  public static class Math
  {
    public static readonly System.Random SystemRandom = new();
    public static Vector3 Vector3(float x = 0, float y = 0, float z = 0)
    {
      return new Vector3(x, y, z);
    }
  }

  public static class Physics
  {
    public static float GetGroundClearance(GroundDetectionResult ground)
    {
      // Snap to ground with small upward offset.
      // Keeps surface normal aligned with slope.
      return (ground.Distance
        * (int)ground.SensorGroundRelation)
        - GroundedPositionUpwardOffset;
    }
  }

  public static class Sensors
  {
    public static GroundDetectionResult? ADetectGround(
      char sensorId,
      UDSensor sensor,
      LayerMask groundLayer)
    {
      var hit = sensor.DownRay.Cast(groundLayer);
      if (hit != null)
      {
        return new(sensorId, hit.Value, sensor.DownRay.Direction, VerticalRelation.Above);
      }

      hit = sensor.UpRay.Cast(groundLayer);
      if (hit != null)
      {
        return new(sensorId, hit.Value, sensor.UpRay.Direction, VerticalRelation.Below);
      }

      return null;
    }

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
          return GroundDetectionResult.CreateABResult(!horizontalDirection, dr1Hit.Value, dr1.Direction, VerticalRelation.Above);
        }
        else
        {
          return GroundDetectionResult.CreateABResult(horizontalDirection, dr2Hit.Value, dr2.Direction, VerticalRelation.Above);
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
          return GroundDetectionResult.CreateABResult(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below);
        }
        else
        {
          return GroundDetectionResult.CreateABResult(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below);
        }
      }

      if (ur1Hit != null)
      {
        return GroundDetectionResult.CreateABResult(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below, checkBalancing());
      }

      if (ur2Hit != null)
      {
        return GroundDetectionResult.CreateABResult(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below, checkBalancing());
      }

      if (dr1Hit != null)
      {
        return GroundDetectionResult.CreateABResult(!horizontalDirection, dr1Hit.Value, dr1.Direction, VerticalRelation.Above, checkBalancing());
      }

      if (dr2Hit != null)
      {
        return GroundDetectionResult.CreateABResult(horizontalDirection, dr2Hit.Value, dr2.Direction, VerticalRelation.Above, checkBalancing());
      }

      return null;
    }

    public static CeilingDetectionResult? CDDetectCeiling(
      UDFSensor c,
      UDFSensor d,
      LayerMask groundLayer,
      bool horizontalDirection)
    {
      SensorRay ur1;
      SensorRay ur2;

      if (horizontalDirection)
      {
        ur1 = c.UpRay;
        ur2 = d.UpRay;
      }
      else
      {
        ur1 = d.UpRay;
        ur2 = c.UpRay;
      }

      var ur1Hit = ur1.Cast(groundLayer);
      var ur2Hit = ur2.Cast(groundLayer);

      if (ur1Hit != null && ur2Hit != null)
      {
        if (ur1Hit.Value.distance <= ur2Hit.Value.distance)
        {
          return CeilingDetectionResult.CreateCDResult(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below);
        }
        else
        {
          return CeilingDetectionResult.CreateCDResult(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below);
        }
      }

      SensorRay dr1;
      SensorRay dr2;

      if (horizontalDirection)
      {
        dr1 = c.DownRay;
        dr2 = d.DownRay;
      }
      else
      {
        dr1 = d.DownRay;
        dr2 = c.DownRay;
      }

      var dr1Hit = dr1.Cast(groundLayer);
      var dr2Hit = dr2.Cast(groundLayer);

      if (dr1Hit != null && dr2Hit != null)
      {
        if (dr1Hit.Value.distance >= dr2Hit.Value.distance)
        {
          return CeilingDetectionResult.CreateCDResult(!horizontalDirection, dr1Hit.Value, dr1.Direction, VerticalRelation.Above);
        }
        else
        {
          return CeilingDetectionResult.CreateCDResult(horizontalDirection, dr2Hit.Value, dr2.Direction, VerticalRelation.Above);
        }
      }

      if (dr1Hit != null)
      {
        return CeilingDetectionResult.CreateCDResult(!horizontalDirection, dr1Hit.Value, dr1.Direction, VerticalRelation.Above);
      }

      if (dr2Hit != null)
      {
        return CeilingDetectionResult.CreateCDResult(horizontalDirection, dr2Hit.Value, dr2.Direction, VerticalRelation.Above);
      }

      if (ur1Hit != null)
      {
        return CeilingDetectionResult.CreateCDResult(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below);
      }

      if (ur2Hit != null)
      {
        return CeilingDetectionResult.CreateCDResult(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below);
      }

      return null;
    }
  }
}
