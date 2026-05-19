using UnityEngine;

public abstract class AIControllerBase<TAIContext>
  : MonoBehaviour,
  IAI<TAIContext>
{
  protected TAIContext _context;

  [SerializeField]
  protected bool _initialized;

  protected AIControllerBase()
  {
    Effects = new();
  }

  public Pipeline Effects { get; }

  public void SetContext(TAIContext context)
  {
    _context = context;
  }
}
