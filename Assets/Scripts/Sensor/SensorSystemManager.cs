public class SensorSystemManager
{
  private SensorSystemInput _input;

  public SensorGroup UpSensorGroup { get; set; }
  public SensorGroup DownSensorGroup { get; set; }
  public SensorGroup LeftSensorGroup { get; set; }
  public SensorGroup RightSensorGroup { get; set; }
  public SensorGroup CurrentSensorGroup { get; private set; }

  public void Update(SensorSystemInput input)
  {
    _input = input;
  }
}
