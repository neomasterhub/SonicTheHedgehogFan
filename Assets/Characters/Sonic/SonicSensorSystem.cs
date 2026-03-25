public class SonicSensorSystem
{
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
