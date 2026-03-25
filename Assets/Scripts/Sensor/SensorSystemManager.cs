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
  }
}
