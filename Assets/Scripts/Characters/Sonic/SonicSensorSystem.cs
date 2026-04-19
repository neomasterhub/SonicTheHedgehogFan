using UnityEngine;
using static SonicConsts.Sizes;

public class SonicSensorSystem
{
  private readonly SonicSensorGroup _bigUpSensorGroup;
  private readonly SonicSensorGroup _bigDownSensorGroup;
  private readonly SonicSensorGroup _bigLeftSensorGroup;
  private readonly SonicSensorGroup _bigRightSensorGroup;
  private readonly SonicSensorGroup _smallUpSensorGroup;
  private readonly SonicSensorGroup _smallDownSensorGroup;
  private readonly SonicSensorGroup _smallLeftSensorGroup;
  private readonly SonicSensorGroup _smallRightSensorGroup;

  private Sensor _o;
  private UDFSensor _a;
  private UDFSensor _b;
  private UDFSensor _c;
  private UDFSensor _d;
  private Vector2 _parentPosition;
  private SonicSensorRayLengths? _sensorRayLengths;

  public SonicSensorSystem(
    SonicSizeMode sizeMode = SonicSizeMode.Big,
    GroundSide groundSide = GroundSide.Down,
    Vector2? parentPosition = null)
  {
    SizeMode = sizeMode;
    GroundSide = groundSide;

    var aColor = Color.softGreen;
    var bColor = Color.green;
    var cColor = Color.softYellow;
    var dColor = Color.yellow;
    var oColor = Color.white;

    _bigDownSensorGroup = new(
      a: new(aColor, new(-Big.HRadius, -Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      b: new(bColor, new(Big.HRadius, -Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      c: new(cColor, new(-Big.HRadius, Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      d: new(dColor, new(Big.HRadius, Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      o: new(oColor, Vector2.zero, Vector2.down),
      parentPosition: parentPosition);
    _bigRightSensorGroup = new(
      a: new(aColor, new(Big.VRadius, -Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      b: new(bColor, new(Big.VRadius, Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      c: new(cColor, new(-Big.VRadius, -Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      d: new(dColor, new(-Big.VRadius, Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      o: new(oColor, Vector2.zero, Vector2.right),
      parentPosition: parentPosition);
    _bigUpSensorGroup = new(
      c: new(cColor, new(-Big.HRadius, -Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      d: new(dColor, new(Big.HRadius, -Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      a: new(aColor, new(-Big.HRadius, Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      b: new(bColor, new(Big.HRadius, Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      o: new(oColor, Vector2.zero, Vector2.up),
      parentPosition: parentPosition);
    _bigLeftSensorGroup = new(
      c: new(cColor, new(Big.VRadius, -Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      d: new(dColor, new(Big.VRadius, Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      a: new(aColor, new(-Big.VRadius, -Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      b: new(bColor, new(-Big.VRadius, Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      o: new(oColor, Vector2.zero, Vector2.left),
      parentPosition: parentPosition);

    SetCurrentSensorGroup();
  }

  public SonicSizeMode SizeMode { get; private set; }
  public GroundSide GroundSide { get; private set; }
  public Vector2 ParentPosition => _parentPosition;

  public void Update(SonicSensorContext context)
  {
    if (SizeMode != context.SizeMode || GroundSide != context.GroundSide)
    {
      _sensorRayLengths = null;

      SizeMode = context.SizeMode;
      GroundSide = context.GroundSide;

      SetCurrentSensorGroup();
    }

    SetParentPosition(context.ParentPosition);
    UpdateSensorActiveStates(context.SensorFlags);
    UpdateSensorRayLengths(context.SensorRayLengths);
  }

  public GroundDetectionResult? DetectGround(bool horizontalDirection, LayerMask groundLayer)
  {
    SensorRay dr1;
    SensorRay dr2;

    if (horizontalDirection)
    {
      dr1 = _a.DownRay;
      dr2 = _b.DownRay;
    }
    else
    {
      dr1 = _b.DownRay;
      dr2 = _a.DownRay;
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
      ur1 = _a.UpRay;
      ur2 = _b.UpRay;
    }
    else
    {
      ur1 = _b.UpRay;
      ur2 = _a.UpRay;
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
      return new(!horizontalDirection, ur1Hit.Value, ur1.Direction, VerticalRelation.Below, IsBalancing(groundLayer));
    }

    if (ur2Hit != null)
    {
      return new(horizontalDirection, ur2Hit.Value, ur2.Direction, VerticalRelation.Below, IsBalancing(groundLayer));
    }

    if (dr1Hit != null)
    {
      return new(!horizontalDirection, dr1Hit.Value, dr1.Direction, VerticalRelation.Above, IsBalancing(groundLayer));
    }

    if (dr2Hit != null)
    {
      return new(horizontalDirection, dr2Hit.Value, dr2.Direction, VerticalRelation.Above, IsBalancing(groundLayer));
    }

    return null;
  }

  public WallDetectionResult? DetectLeftWall(LayerMask groundLayer)
  {
    return DetectWall(_c.FrontRay, _a.FrontRay, groundLayer);
  }

  public WallDetectionResult? DetectRightWall(LayerMask groundLayer)
  {
    return DetectWall(_d.FrontRay, _b.FrontRay, groundLayer);
  }

  public void Draw()
  {
    _o.Draw();
    _a.Draw();
    _b.Draw();
    _c.Draw();
    _d.Draw();
  }

  private void SetCurrentSensorGroup()
  {
    var currentSensorGroup = SizeMode switch
    {
      SonicSizeMode.Big => GroundSide switch
      {
        GroundSide.Up => _bigUpSensorGroup,
        GroundSide.Down => _bigDownSensorGroup,
        GroundSide.Left => _bigLeftSensorGroup,
        GroundSide.Right => _bigRightSensorGroup,
        _ => throw GroundSide.ArgumentOutOfRangeException(),
      },
      SonicSizeMode.Small => GroundSide switch
      {
        GroundSide.Up => _smallUpSensorGroup,
        GroundSide.Down => _smallDownSensorGroup,
        GroundSide.Left => _smallLeftSensorGroup,
        GroundSide.Right => _smallRightSensorGroup,
        _ => throw GroundSide.ArgumentOutOfRangeException(),
      },
      _ => throw SizeMode.ArgumentOutOfRangeException(),
    };

    _o = currentSensorGroup.O;
    _a = currentSensorGroup.A;
    _b = currentSensorGroup.B;
    _c = currentSensorGroup.C;
    _d = currentSensorGroup.D;
  }

  public void SetParentPosition(Vector2 parentPosition)
  {
    _parentPosition = parentPosition;
    _o.SetParentPosition(parentPosition);
    _a.SetParentPosition(parentPosition);
    _b.SetParentPosition(parentPosition);
    _c.SetParentPosition(parentPosition);
    _d.SetParentPosition(parentPosition);
  }

  public void UpdateSensorActiveStates(SonicSensorFlags flags)
  {
    _o.Enabled = flags.CheckBalancing;

    _c.UpRay.Enabled = flags.CheckCeiling;
    _c.DownRay.Enabled = flags.CheckCeiling;
    _d.UpRay.Enabled = flags.CheckCeiling;
    _d.DownRay.Enabled = flags.CheckCeiling;

    _a.UpRay.Enabled = flags.CheckGround;
    _a.DownRay.Enabled = flags.CheckGround;
    _b.UpRay.Enabled = flags.CheckGround;
    _b.DownRay.Enabled = flags.CheckGround;
  }

  private void UpdateSensorRayLengths(SonicSensorRayLengths lengths)
  {
    if (_sensorRayLengths != lengths)
    {
      _sensorRayLengths = lengths;

      _o.Ray.Length = lengths.O;
      UpdateUDFSensorRayLengths(_c, lengths.TopUDF);
      UpdateUDFSensorRayLengths(_d, lengths.TopUDF);
      UpdateUDFSensorRayLengths(_a, lengths.BottomUDF);
      UpdateUDFSensorRayLengths(_b, lengths.BottomUDF);
    }
  }

  private void UpdateUDFSensorRayLengths(UDFSensor sensor, Vector3 udfLengths)
  {
    sensor.UpRay.Length = udfLengths.x;
    sensor.DownRay.Length = udfLengths.y;
    sensor.FrontRay.Length = udfLengths.z;
  }

  private bool IsBalancing(LayerMask groundLayer)
  {
    return _o.Enabled && !_o.Ray.Cast(groundLayer).HasValue;
  }

  private RaycastHit2D? GetClosestWallHit(SensorRay topRay, SensorRay bottomRay, LayerMask groundLayer)
  {
    var topHit = topRay.Cast(groundLayer);
    var bottomHit = bottomRay.Cast(groundLayer);

    if (topHit == null)
    {
      return bottomHit;
    }

    if (bottomHit == null)
    {
      return topHit;
    }

    return topHit.Value.distance <= bottomHit.Value.distance ? topHit : bottomHit;
  }

  private WallDetectionResult? DetectWall(SensorRay topRay, SensorRay bottomRay, LayerMask groundLayer)
  {
    var hit = GetClosestWallHit(topRay, bottomRay, groundLayer);
    if (hit == null)
    {
      return null;
    }

    return new(hit.Value.distance, Vector2.SignedAngle(-topRay.Direction, hit.Value.normal));
  }
}
