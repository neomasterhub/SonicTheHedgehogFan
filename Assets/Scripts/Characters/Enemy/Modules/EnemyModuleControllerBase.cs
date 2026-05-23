using UnityEngine;

[RequireComponent(typeof(EnemyControllerBase))]
public abstract class EnemyModuleControllerBase
  : MonoBehaviour,
  IEnemyModule
{
  protected IEnemyContext _context;

  private void Awake()
  {
    Initialize(GetComponent<EnemyControllerBase>());
  }

  public virtual void Initialize(IEnemyContext context)
  {
    _context = context;
  }

  public abstract void Apply();
}
