using Unity.Cinemachine;

/// <summary>
/// Init.
/// </summary>
public partial class CameraController
{
  private void Awake()
  {
    var cmObj = transform.Find("Cinemachine Camera").gameObject;
    _cm = cmObj.GetComponent<CinemachineCamera>();
  }
}
