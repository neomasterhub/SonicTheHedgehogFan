using UnityEngine;

public class SonicSensorGroup
{
  public SonicSensorGroup(UDFSensor a, UDFSensor b, UDFSensor c, UDFSensor d)
  {
    A = a;
    B = b;
    C = c;
    D = d;
  }

  public UDFSensor A { get; }
  public UDFSensor B { get; }
  public UDFSensor C { get; }
  public UDFSensor D { get; }

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
