public class SensorSystemManager
{
  private SensorSystemInput _input;

  public SensorGroup BigUpSensorGroup { get; set; }
  public SensorGroup BigDownSensorGroup { get; set; }
  public SensorGroup BigLeftSensorGroup { get; set; }
  public SensorGroup BigRightSensorGroup { get; set; }
  public SensorGroup SmallUpSensorGroup { get; set; }
  public SensorGroup SmallDownSensorGroup { get; set; }
  public SensorGroup SmallLeftSensorGroup { get; set; }
  public SensorGroup SmallRightSensorGroup { get; set; }
  public SensorGroup CurrentSensorGroup { get; private set; }

  public void Update(SensorSystemInput input)
  {
    _input = input;

    CurrentSensorGroup = _input.SizeMode switch
    {
      SizeMode.Big => _input.GroundSide switch
      {
        GroundSide.Up => BigUpSensorGroup,
        GroundSide.Down => BigDownSensorGroup,
        GroundSide.Left => BigLeftSensorGroup,
        GroundSide.Right => BigRightSensorGroup,
        _ => throw _input.GroundSide.ArgumentOutOfRangeException(),
      },
      SizeMode.Small => _input.GroundSide switch
      {
        GroundSide.Up => SmallUpSensorGroup,
        GroundSide.Down => SmallDownSensorGroup,
        GroundSide.Left => SmallLeftSensorGroup,
        GroundSide.Right => SmallRightSensorGroup,
        _ => throw _input.GroundSide.ArgumentOutOfRangeException(),
      },
      _ => throw _input.SizeMode.ArgumentOutOfRangeException(),
    };
  }
}
