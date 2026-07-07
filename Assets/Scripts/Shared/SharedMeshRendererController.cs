using UnityEngine;

public class SharedMeshRendererController : MonoBehaviour
{
  private Material _material;
  private Mesh _mesh;

  public static SharedMeshRendererController Instance { get; private set; }

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    InitializeComponents();
  }

  private void OnDestroy()
  {
    if (Instance == this)
    {
      Instance = null;
    }

    DestroyComponents();
  }

  private void InitializeComponents()
  {
    _material = new(Shader.Find("Sprites/Default"))
    {
      hideFlags = HideFlags.HideAndDontSave,
    };

    _mesh = new()
    {
      hideFlags = HideFlags.HideAndDontSave,
    };

    _mesh.MarkDynamic();
  }

  private void DestroyComponents()
  {
    if (_material != null)
    {
      Destroy(_material);
      _material = null;
    }

    if (_mesh != null)
    {
      Destroy(_mesh);
      _mesh = null;
    }
  }
}
