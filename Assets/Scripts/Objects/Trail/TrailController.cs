using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class TrailController : MonoBehaviour
{
  private int _lastUpdateFrame;
  private Vector3 _position;
  private Transform _target;

  [SerializeField]
  private int _delayFrameCount;
}
