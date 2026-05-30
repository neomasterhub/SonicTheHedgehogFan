using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class AnimalController : MonoBehaviour
{
  private readonly ASensorSystem _sensorSystem;
  private readonly AnimalSpeedSystem _speedSystem;

  private bool _isGrounded;
  private Animator _animator;
  private GroundDetectionResult _lastGroundDetectionResult;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private bool _horizontalDirection;
  [SerializeField]
  private float _gravitySpeedSpx;
  [SerializeField]
  private float _horizontalSpeedPx;
  [SerializeField]
  private float _jumpSpeedPx;
  [SerializeField]
  private float _releaseSpeedPx;
}
