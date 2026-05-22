using UnityEngine;

[RequireComponent(typeof(IEnemyContext))]
public abstract class EnemyModuleControllerBase
  : MonoBehaviour,
  IEnemyModule
{
  protected IEnemyContext _context;

  private void Awake()
  {
    Initialize(GetComponent<IEnemyContext>());
  }

  public virtual void Initialize(IEnemyContext context)
  {
    _context = context;
  }

  public abstract void Apply();
}
