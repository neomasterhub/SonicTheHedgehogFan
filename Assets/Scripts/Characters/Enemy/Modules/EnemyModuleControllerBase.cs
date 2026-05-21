using UnityEngine;

[RequireComponent(typeof(IEnemyContext))]
public abstract class EnemyModuleControllerBase
  : MonoBehaviour,
  IEnemyModule
{
  protected IEnemyContext _context;

  protected virtual void Awake()
  {
    Initialize(GetComponent<IEnemyContext>());
  }

  public virtual void Initialize(IEnemyContext context)
  {
    _context = context;
  }

  public abstract void Apply();
}
