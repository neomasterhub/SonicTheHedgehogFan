using UnityEngine;

public readonly struct PlayerSensorSystemInput<TSizeMode>
  where TSizeMode : struct
{
  public readonly bool HorizontalDirection;
  public readonly Vector2 ParentPosition;
  public readonly TSizeMode SizeMode;
  public readonly GroundSide GroundSide;
  public readonly LayerMask GroundLayer;
  public readonly SensorSettings ASettings;
  public readonly SensorSettings BSettings;
  public readonly SensorSettings CSettings;
  public readonly SensorSettings DSettings;
  public readonly SensorSettings ESettings;
  public readonly SensorSettings FSettings;

  public SensorSettings this[SensorId id]
  {
    get
    {
      return id switch
      {
        SensorId.A => ASettings,
        SensorId.B => BSettings,
        SensorId.C => CSettings,
        SensorId.D => DSettings,
        SensorId.E => ESettings,
        SensorId.F => FSettings,
        _ => throw id.ArgumentOutOfRangeException(),
      };
    }
  }
}
