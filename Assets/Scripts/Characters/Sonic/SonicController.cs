using UnityEngine;

public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sensorSystem = new();
  private readonly TimerSystem _timerSystem = new();

  private PlayerInputSystem _inputSystem;

  // Flags
  private GroundSide _groundSide;
  private SonicSizeMode _sizeMode;
  private bool _postWallDetachInputLock;

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);

  private void OnDrawGizmos()
  {
    _sensorSystem.CurrentSensorGroup.Draw();
  }

  private void Awake()
  {
    _inputSystem = new(
      () => Input.GetAxis(SharedConsts.InputAxis.Horizontal),
      () => Input.GetAxis(SharedConsts.InputAxis.Vertical));
  }

  private void FixedUpdate()
  {
    UpdateSystems();
  }

  private void UpdateSystems()
  {
    _inputSystem.Update(!_postWallDetachInputLock);
    _sensorSystem.Update(_sizeMode, _groundSide, transform.position, TopUDFLengths, BottomUDFLengths);
    _timerSystem.Update(Time.deltaTime);
  }
}
