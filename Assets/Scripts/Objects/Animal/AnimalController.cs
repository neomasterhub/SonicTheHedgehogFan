using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
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
  private float _releaseSpeedPx;
  [SerializeField]
  private float _gravitySpeedSpx;
}
