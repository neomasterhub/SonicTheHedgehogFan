public class SensorGroup
{
  public UDFSensor A { get; set; }
  public UDFSensor B { get; set; }
  public UDFSensor C { get; set; }
  public UDFSensor D { get; set; }

  public void Draw()
  {
    A.Draw();
    B.Draw();
    C.Draw();
    D.Draw();
  }
}
