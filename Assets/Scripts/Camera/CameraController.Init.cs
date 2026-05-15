using Unity.Cinemachine;

/// <summary>
/// Init.
/// </summary>
public partial class CameraController
{
  public CameraController()
  {
    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _target = _targetObj.GetComponent<ICameraTarget>();

    var cmObj = transform.Find("Cinemachine Camera").gameObject;
    _cm = cmObj.GetComponent<CinemachineCamera>();
  }
}
