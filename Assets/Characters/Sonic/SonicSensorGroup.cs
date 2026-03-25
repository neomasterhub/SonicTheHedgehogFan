using UnityEngine;

public class SonicSensorGroup
{
  public UDFSensor A { get; set; }
  public UDFSensor B { get; set; }
  public UDFSensor C { get; set; }
  public UDFSensor D { get; set; }

  public void SetSensorPositions(Vector2 parentPosition)
  {
    A.SetPosition(parentPosition);
    B.SetPosition(parentPosition);
    C.SetPosition(parentPosition);
    D.SetPosition(parentPosition);
  }

  public void Draw()
  {
    A.Draw();
    B.Draw();
    C.Draw();
    D.Draw();
  }
}
