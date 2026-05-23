using UnityEngine;

[RequireComponent(typeof(EnemyControllerBase))]
public abstract class EnemyModuleControllerBase
  : MonoBehaviour,
  ISystemModule<EnemyControllerBase>
{
  protected EnemyControllerBase _context;

  private void Awake()
  {
    Initialize(GetComponent<EnemyControllerBase>());
  }

  public virtual void Initialize(EnemyControllerBase context)
  {
    _context = context;
  }

  public abstract void Apply();
}
