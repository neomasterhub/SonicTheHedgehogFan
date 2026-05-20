using System;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public partial class UDFEnemySensorSystemController
  : MonoBehaviour,
  IEnemySensorSystem
{
  private UDFSensor _o;
  private Action _updateNext;
  private WallDetectionResult? _wall;
  private GroundDetectionResult? _ground;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private Vector2 _position;
}
