using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class TrailController : MonoBehaviour
{
  private float _timer;
  private Vector3 _position;
  private Transform _target;

  [SerializeField]
  private float _delay;
}
