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

  private Vector3? _topUDFLengths;
  private Vector3? _bottomUDFLengths;

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
  public SonicSensorGroup CurrentSensorGroup { get; private set; }
  public Vector2 ParentPosition
  {
    get => CurrentSensorGroup.ParentPosition;
    set => CurrentSensorGroup.ParentPosition = value;
  }

  public void Update(
    SonicSizeMode sizeMode,
    GroundSide groundSide,
    Vector2 parentPosition,
    Vector3 topUDFLengths,
    Vector3 bottomUDFLengths)
  {
    if (SizeMode != sizeMode || GroundSide != groundSide)
    {
      _topUDFLengths = null;
      _bottomUDFLengths = null;

      SizeMode = sizeMode;
      GroundSide = groundSide;

      SetCurrentSensorGroup();

      CurrentSensorGroup.O.Enabled = sizeMode == SonicSizeMode.Big;
    }

    ParentPosition = parentPosition;

    if (_topUDFLengths != topUDFLengths)
    {
      _topUDFLengths = topUDFLengths;
      UpdateUDFSensorLengths(CurrentSensorGroup.C, topUDFLengths);
      UpdateUDFSensorLengths(CurrentSensorGroup.D, topUDFLengths);
    }

    if (_bottomUDFLengths != bottomUDFLengths)
    {
      _bottomUDFLengths = bottomUDFLengths;
      CurrentSensorGroup.O.Ray.Length = topUDFLengths.y + Big.VRadius;
      UpdateUDFSensorLengths(CurrentSensorGroup.A, bottomUDFLengths);
      UpdateUDFSensorLengths(CurrentSensorGroup.B, bottomUDFLengths);
    }
  }

  public GroundDetectionResult? DetectGround(
    bool horizontalDirection,
    LayerMask groundLayer)
  {
    SensorRay dr1;
    SensorRay dr2;

    if (horizontalDirection)
    {
      dr1 = CurrentSensorGroup.A.DownRay;
      dr2 = CurrentSensorGroup.B.DownRay;
    }
    else
    {
      dr1 = CurrentSensorGroup.B.DownRay;
      dr2 = CurrentSensorGroup.A.DownRay;
    }

    var dr1Hit = dr1.Cast(groundLayer);
    var dr2Hit = dr2.Cast(groundLayer);

    if (dr1Hit != null && dr2Hit != null)
    {
      if (dr1Hit.Value.distance <= dr2Hit.Value.distance)
      {
        return new(dr1Hit.Value, dr1.Direction);
      }
      else
      {
        return new(dr2Hit.Value, dr2.Direction);
      }
    }

    SensorRay ur1;
    SensorRay ur2;

    if (horizontalDirection)
    {
      ur1 = CurrentSensorGroup.A.UpRay;
      ur2 = CurrentSensorGroup.B.UpRay;
    }
    else
    {
      ur1 = CurrentSensorGroup.B.UpRay;
      ur2 = CurrentSensorGroup.A.UpRay;
    }

    var ur1Hit = ur1.Cast(groundLayer);
    var ur2Hit = ur2.Cast(groundLayer);

    if (ur1Hit != null && ur2Hit != null)
    {
      if (ur1Hit.Value.distance >= ur2Hit.Value.distance)
      {
        return new(ur1Hit.Value, ur1.Direction, VerticalSide.Below);
      }
      else
      {
        return new(ur2Hit.Value, ur2.Direction, VerticalSide.Below);
      }
    }

    if (ur1Hit != null)
    {
      return new(ur1Hit.Value, ur1.Direction, VerticalSide.Below);
    }

    if (ur2Hit != null)
    {
      return new(ur2Hit.Value, ur2.Direction, VerticalSide.Below);
    }

    if (dr1Hit != null)
    {
      return new(dr1Hit.Value, dr1.Direction);
    }

    if (dr2Hit != null)
    {
      return new(dr2Hit.Value, dr2.Direction);
    }

    return null;
  }

  public void Draw()
  {
    CurrentSensorGroup.Draw();
  }

  private void SetCurrentSensorGroup()
  {
    CurrentSensorGroup = SizeMode switch
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
  }

  private void UpdateUDFSensorLengths(UDFSensor sensor, Vector3 udfLengths)
  {
    sensor.UpRay.Length = udfLengths.x;
    sensor.DownRay.Length = udfLengths.y;
    sensor.FrontRay.Length = udfLengths.z;
  }
}
