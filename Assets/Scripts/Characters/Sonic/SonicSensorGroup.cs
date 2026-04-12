using UnityEngine;

public class SonicSensorGroup
{
  private Vector2 _parentPosition;

  public SonicSensorGroup(
    UDFSensor a,
    UDFSensor b,
    UDFSensor c,
    UDFSensor d,
    Sensor o,
    Vector2? parentPosition = null)
  {
    A = a;
    B = b;
    C = c;
    D = d;
    O = o;
    ParentPosition = parentPosition ?? Vector2.zero;
  }

  public UDFSensor A { get; }
  public UDFSensor B { get; }
  public UDFSensor C { get; }
  public UDFSensor D { get; }
  public Sensor O { get; }

  public Vector2 ParentPosition
  {
    get => _parentPosition;
    set
    {
      if (_parentPosition != value)
      {
        _parentPosition = value;
        A.SetParentPosition(value);
        B.SetParentPosition(value);
        C.SetParentPosition(value);
        D.SetParentPosition(value);
        O.SetParentPosition(value);
      }
    }
  }

  public void Draw()
  {
    A.Draw();
    B.Draw();
    C.Draw();
    D.Draw();
    O.Draw();
  }
}
