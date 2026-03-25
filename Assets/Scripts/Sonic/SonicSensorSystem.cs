public class SonicSensorSystem
{
  public SensorGroup BigUpSensorGroup { get; set; }
  public SensorGroup BigDownSensorGroup { get; set; }
  public SensorGroup BigLeftSensorGroup { get; set; }
  public SensorGroup BigRightSensorGroup { get; set; }
  public SensorGroup SmallUpSensorGroup { get; set; }
  public SensorGroup SmallDownSensorGroup { get; set; }
  public SensorGroup SmallLeftSensorGroup { get; set; }
  public SensorGroup SmallRightSensorGroup { get; set; }
  public SensorGroup CurrentSensorGroup { get; private set; }

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
