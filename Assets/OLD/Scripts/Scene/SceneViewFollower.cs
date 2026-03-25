#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SceneViewFollower : MonoBehaviour
{
  private Vector3 _lastTargetPosition;

  public Transform Target;

  private void Update()
  {
    var sceneView = SceneView.lastActiveSceneView;

    if (!sceneView || !Target)
    {
      return;
    }

    var targetPosition = Target.position;
    if (targetPosition == _lastTargetPosition)
    {
      return;
    }

    sceneView.LookAt(targetPosition, sceneView.rotation, sceneView.size);
    _lastTargetPosition = targetPosition;
  }
}
#endif
