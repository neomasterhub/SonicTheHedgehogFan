using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.InputAxis;

public class SonicController : MonoBehaviour
{
  private readonly PlayerInputSystem _inputSystem = new(
    () => Input.GetAxis(Horizontal),
    () => Input.GetAxis(Vertical));
  private readonly SonicSensorSystem _sensorSystem = new();
  private readonly TimerSystem _timerSystem = new();

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
    Application.targetFrameRate = FramePerSec;
    Time.fixedDeltaTime = 1f / FramePerSec;
  }

  private void FixedUpdate()
  {
    UpdateSystems();
  }

  private void UpdateSystems()
  {
    _timerSystem.Update(Time.deltaTime);
    _inputSystem.Update(!_postWallDetachInputLock);
    _sensorSystem.Update(_sizeMode, _groundSide, transform.position, TopUDFLengths, BottomUDFLengths);
  }
}
