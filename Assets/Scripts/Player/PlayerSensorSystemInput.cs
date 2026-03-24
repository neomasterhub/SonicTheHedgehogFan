using UnityEngine;

public readonly struct PlayerSensorSystemInput
{
  public readonly bool HorizontalDirection;
  public readonly Vector2 Parent;
  public readonly SizeMode SizeMode;
  public readonly GroundSide GroundSide;
  public readonly LayerMask GroundLayer;
  public readonly SensorSettings ASettings;
  public readonly SensorSettings BSettings;
  public readonly SensorSettings CSettings;
  public readonly SensorSettings DSettings;
  public readonly SensorSettings ESettings;
  public readonly SensorSettings FSettings;

  public PlayerSensorSystemInput(bool horizontalDirection, Vector2 parent, SizeMode sizeMode, GroundSide groundSide, LayerMask groundLayer, SensorSettings aSettings, SensorSettings bSettings, SensorSettings cSettings, SensorSettings dSettings, SensorSettings eSettings, SensorSettings fSettings)
  {
    HorizontalDirection = horizontalDirection;
    Parent = parent;
    SizeMode = sizeMode;
    GroundSide = groundSide;
    GroundLayer = groundLayer;
    ASettings = aSettings;
    BSettings = bSettings;
    CSettings = cSettings;
    DSettings = dSettings;
    ESettings = eSettings;
    FSettings = fSettings;
  }
}
