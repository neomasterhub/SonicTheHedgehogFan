using UnityEngine;
using static SonicConsts;

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

  private Vector3 _topUDFLengths;
  private Vector3 _bottomUDFLengths;

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
      a: new(aColor, new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      b: new(bColor, new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      c: new(cColor, new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      d: new(dColor, new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      parentPosition: parentPosition);
    _bigRightSensorGroup = new(
      a: new(aColor, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      b: new(bColor, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      c: new(cColor, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      d: new(dColor, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      parentPosition: parentPosition);
    _bigUpSensorGroup = new(
      c: new(cColor, new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      d: new(dColor, new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      a: new(aColor, new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      b: new(bColor, new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      parentPosition: parentPosition);
    _bigLeftSensorGroup = new(
      c: new(cColor, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      d: new(dColor, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      a: new(aColor, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      b: new(bColor, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
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
      SizeMode = sizeMode;
      GroundSide = groundSide;
      SetCurrentSensorGroup();
    }

    ParentPosition = parentPosition;

    if (_bottomUDFLengths != bottomUDFLengths)
    {
      _bottomUDFLengths = bottomUDFLengths;
      CurrentSensorGroup.A.UpRay.Length = bottomUDFLengths.x;
      CurrentSensorGroup.A.DownRay.Length = bottomUDFLengths.y;
      CurrentSensorGroup.A.FrontRay.Length = bottomUDFLengths.z;
      CurrentSensorGroup.B.UpRay.Length = bottomUDFLengths.x;
      CurrentSensorGroup.B.DownRay.Length = bottomUDFLengths.y;
      CurrentSensorGroup.B.FrontRay.Length = bottomUDFLengths.z;
    }

    if (_topUDFLengths != topUDFLengths)
    {
      _topUDFLengths = topUDFLengths;
      CurrentSensorGroup.C.UpRay.Length = topUDFLengths.x;
      CurrentSensorGroup.C.DownRay.Length = topUDFLengths.y;
      CurrentSensorGroup.C.FrontRay.Length = topUDFLengths.z;
      CurrentSensorGroup.D.UpRay.Length = topUDFLengths.x;
      CurrentSensorGroup.D.DownRay.Length = topUDFLengths.y;
      CurrentSensorGroup.D.FrontRay.Length = topUDFLengths.z;
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
}
