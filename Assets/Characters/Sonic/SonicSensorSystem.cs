using UnityEngine;
using static SonicConsts;

public class SonicSensorSystem
{
  public SonicSensorSystem()
  {
    BigDownSensorGroup = new()
    {
      A = new(Color.limeGreen, new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      B = new(Color.limeGreen, new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      C = new(Color.green, new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      D = new(Color.green, new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
    };
    BigRightSensorGroup = new()
    {
      A = new(Color.limeGreen, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      B = new(Color.limeGreen, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      C = new(Color.green, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      D = new(Color.green, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
    };
    BigUpSensorGroup = new()
    {
      C = new(Color.limeGreen, new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      D = new(Color.limeGreen, new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      A = new(Color.green, new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      B = new(Color.green, new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
    };
    BigRightSensorGroup = new()
    {
      C = new(Color.limeGreen, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      D = new(Color.limeGreen, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      A = new(Color.green, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      B = new(Color.green, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
    };
  }

  public SonicSensorGroup BigUpSensorGroup { get; set; }
  public SonicSensorGroup BigDownSensorGroup { get; set; }
  public SonicSensorGroup BigLeftSensorGroup { get; set; }
  public SonicSensorGroup BigRightSensorGroup { get; set; }
  public SonicSensorGroup SmallUpSensorGroup { get; set; }
  public SonicSensorGroup SmallDownSensorGroup { get; set; }
  public SonicSensorGroup SmallLeftSensorGroup { get; set; }
  public SonicSensorGroup SmallRightSensorGroup { get; set; }
  public SonicSensorGroup CurrentSensorGroup { get; private set; }

  public void SetCurrentSensorGroup(SonicSizeMode sizeMode, GroundSide groundSide)
  {
    CurrentSensorGroup = sizeMode switch
    {
      SonicSizeMode.Big => groundSide switch
      {
        GroundSide.Up => BigUpSensorGroup,
        GroundSide.Down => BigDownSensorGroup,
        GroundSide.Left => BigLeftSensorGroup,
        GroundSide.Right => BigRightSensorGroup,
        _ => throw groundSide.ArgumentOutOfRangeException(),
      },
      SonicSizeMode.Small => groundSide switch
      {
        GroundSide.Up => SmallUpSensorGroup,
        GroundSide.Down => SmallDownSensorGroup,
        GroundSide.Left => SmallLeftSensorGroup,
        GroundSide.Right => SmallRightSensorGroup,
        _ => throw groundSide.ArgumentOutOfRangeException(),
      },
      _ => throw sizeMode.ArgumentOutOfRangeException(),
    };
  }
}
