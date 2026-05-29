using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class AnimalController : MonoBehaviour
{
  private readonly ASensorSystem _sensorSystem;
  private readonly AnimalSpeedSystem _speedSystem;

  private bool _isGrounded;
  private GroundDetectionResult _lastGroundDetectionResult;

  [SerializeField]
  private float _speedXPx;
  [SerializeField]
  private float _jumpSpeedPx;
  [SerializeField]
  private float _releasedSpeedPx;
  [SerializeField]
  private float _gravitySpeedSpx;
}
