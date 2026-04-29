#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InspectorLabelAttribute))]
public class InspectorLabelDrawer : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    var attr = (InspectorLabelAttribute)attribute;

    label.text = attr.Label;

    EditorGUI.PropertyField(position, property, label, true);
  }
}
#endif
