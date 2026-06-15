using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public partial class ABSensorBlockModuleController
  : BlockModuleControllerBase
{
  private readonly UDFSensor _a;
  private readonly UDFSensor _b;

  private SpriteRenderer _spriteRenderer;
  private float _maxLeftPushSpeed;
  private float _maxRightPushSpeed;

  [SerializeField]
  private Vector2 _position;
  [SerializeField]
  [InspectorLabel("A-B Distance")]
  private float _abDistance;
  [SerializeField]
  private float _wallClearance;
}
