using UnityEngine;

public class SharedMeshRendererController : MonoBehaviour
{
  public static SharedMeshRendererController Instance { get; private set; }

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  private void OnDestroy()
  {
    if (Instance == this)
    {
      Instance = null;
    }
  }
}
