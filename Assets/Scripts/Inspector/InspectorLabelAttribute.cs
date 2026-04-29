using UnityEngine;

public class InspectorLabelAttribute : PropertyAttribute
{
  public readonly string Label;

  public InspectorLabelAttribute(string label)
  {
    Label = label;
  }
}
