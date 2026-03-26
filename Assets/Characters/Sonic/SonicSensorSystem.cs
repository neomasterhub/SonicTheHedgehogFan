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

  public SonicSensorSystem(
    SonicSizeMode sizeMode = SonicSizeMode.Big,
    GroundSide groundSide = GroundSide.Down,
    Vector2? parentPosition = null)
  {
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
    _bigRightSensorGroup = new(
      c: new(cColor, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      d: new(dColor, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      a: new(aColor, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      b: new(bColor, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      parentPosition: parentPosition);

    SetCurrentSensorGroup(sizeMode, groundSide);
  }

  public SonicSizeMode SizeMode { get; private set; }
  public GroundSide GroundSide { get; private set; }
  public SonicSensorGroup CurrentSensorGroup { get; private set; }

  public void UpdateCurrentSensorGroup(SonicSizeMode sizeMode, GroundSide groundSide)
  {
    if (SizeMode == sizeMode && GroundSide == groundSide)
    {
      return;
    }

    SizeMode = sizeMode;
    GroundSide = groundSide;

    SetCurrentSensorGroup(sizeMode, groundSide);
  }

  private void SetCurrentSensorGroup(SonicSizeMode sizeMode, GroundSide groundSide)
  {
    CurrentSensorGroup = sizeMode switch
    {
      SonicSizeMode.Big => groundSide switch
      {
        GroundSide.Up => _bigUpSensorGroup,
        GroundSide.Down => _bigDownSensorGroup,
        GroundSide.Left => _bigLeftSensorGroup,
        GroundSide.Right => _bigRightSensorGroup,
        _ => throw groundSide.ArgumentOutOfRangeException(),
      },
      SonicSizeMode.Small => groundSide switch
      {
        GroundSide.Up => _smallUpSensorGroup,
        GroundSide.Down => _smallDownSensorGroup,
        GroundSide.Left => _smallLeftSensorGroup,
        GroundSide.Right => _smallRightSensorGroup,
        _ => throw groundSide.ArgumentOutOfRangeException(),
      },
      _ => throw sizeMode.ArgumentOutOfRangeException(),
    };
  }
}
