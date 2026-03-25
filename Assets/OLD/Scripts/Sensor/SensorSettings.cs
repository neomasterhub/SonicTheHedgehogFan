using UnityEngine;

public struct SensorSettings
{
  public bool Enabled;
  public float Length;
  public float ReversedLength;
  public Color EnabledColor;
  public Color DisabledColor;

  public SensorSettings(float length, float reversedLength, Color enabledColor, Color disabledColor, bool enabled = true)
  {
    Length = length;
    ReversedLength = reversedLength;
    EnabledColor = enabledColor;
    DisabledColor = disabledColor;
    Enabled = enabled;
  }

  public SensorSettings Enable(bool enabled)
  {
    Enabled = enabled;
    return this;
  }

  public SensorSettings SetLengths(float length, float reversedLength)
  {
    Length = length;
    ReversedLength = reversedLength;
    return this;
  }
}
