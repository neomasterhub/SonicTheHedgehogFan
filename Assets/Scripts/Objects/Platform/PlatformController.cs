using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class PlatformController : MonoBehaviour
{
  private IPlatformObject[] _platformObjects;

  [SerializeField]
  [InspectorLabel("Platform Objects")]
  private GameObject[] _platformObjectObjs;
}
