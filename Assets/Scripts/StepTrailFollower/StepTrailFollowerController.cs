using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class StepTrailFollowerController : MonoBehaviour
{
  private float _stepTime;
  private Vector3 _position;

  [SerializeField]
  private Transform _parentTransform;
  [SerializeField]
  private Vector3 _origin;
  [SerializeField]
  private float _stepDuration;
}
