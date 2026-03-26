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

    _bigDownSensorGroup = new(
      a: new(aColor, new(-Big.HRadius, -Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      b: new(bColor, new(Big.HRadius, -Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      c: new(cColor, new(-Big.HRadius, Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      d: new(dColor, new(Big.HRadius, Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      parentPosition: parentPosition);
    _bigRightSensorGroup = new(
      a: new(aColor, new(Big.VRadius, -Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      b: new(bColor, new(Big.VRadius, Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      c: new(cColor, new(-Big.VRadius, -Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      d: new(dColor, new(-Big.VRadius, Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      parentPosition: parentPosition);
    _bigUpSensorGroup = new(
      c: new(cColor, new(-Big.HRadius, -Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      d: new(dColor, new(Big.HRadius, -Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      a: new(aColor, new(-Big.HRadius, Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      b: new(bColor, new(Big.HRadius, Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      parentPosition: parentPosition);
    _bigLeftSensorGroup = new(
      c: new(cColor, new(Big.VRadius, -Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      d: new(dColor, new(Big.VRadius, Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      a: new(aColor, new(-Big.VRadius, -Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      b: new(bColor, new(-Big.VRadius, Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
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
      UpdateUDFSensorLengths(CurrentSensorGroup.A, bottomUDFLengths);
      UpdateUDFSensorLengths(CurrentSensorGroup.B, bottomUDFLengths);
    }
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
