using UnityEngine;

public class SonicSensorGroup
{
  public UDFSensor A { get; set; }
  public UDFSensor B { get; set; }
  public UDFSensor C { get; set; }
  public UDFSensor D { get; set; }

  public void SetParentPosition(Vector2 parentPosition)
  {
    A.SetParentPosition(parentPosition);
    B.SetParentPosition(parentPosition);
    C.SetParentPosition(parentPosition);
    D.SetParentPosition(parentPosition);
  }

  public void Draw()
  {
    A.Draw();
    B.Draw();
    C.Draw();
    D.Draw();
  }
}
