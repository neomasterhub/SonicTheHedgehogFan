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

  public SonicSensorSystem()
  {
    _bigDownSensorGroup = new()
    {
      A = new(Color.limeGreen, new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      B = new(Color.limeGreen, new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
      C = new(Color.green, new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.left),
      D = new(Color.green, new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up, Vector2.down, Vector2.right),
    };
    _bigRightSensorGroup = new()
    {
      A = new(Color.limeGreen, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      B = new(Color.limeGreen, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
      C = new(Color.green, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.down),
      D = new(Color.green, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left, Vector2.right, Vector2.up),
    };
    _bigUpSensorGroup = new()
    {
      C = new(Color.limeGreen, new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      D = new(Color.limeGreen, new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
      A = new(Color.green, new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.right),
      B = new(Color.green, new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.down, Vector2.up, Vector2.left),
    };
    _bigRightSensorGroup = new()
    {
      C = new(Color.limeGreen, new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      D = new(Color.limeGreen, new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
      A = new(Color.green, new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.up),
      B = new(Color.green, new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right, Vector2.left, Vector2.down),
    };
  }

  public SonicSensorGroup CurrentSensorGroup { get; private set; }

  public void SetCurrentSensorGroup(SonicSizeMode sizeMode, GroundSide groundSide)
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
