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
  private float? _speedX;
  [SerializeField]
  private float? _jumpSpeed;
  [SerializeField]
  private float? _gravitySpeed;
  [SerializeField]
  private Vector2 _initialSpeed;
}
