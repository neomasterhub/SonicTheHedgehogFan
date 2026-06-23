using UnityEngine;

[DefaultExecutionOrder(1)]
public abstract class ModuleControllerBase<TCore>
  : MonoBehaviour,
  ISystemModule<TCore>
  where TCore : MonoBehaviour
{
  protected TCore _context;

  private void Awake()
  {
    Initialize(GetComponent<TCore>());
  }

  public virtual void Initialize(TCore context)
  {
    _context = context;
  }

  public abstract void Apply();
}
