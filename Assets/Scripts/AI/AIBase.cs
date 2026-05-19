public abstract class AIBase<TAIContext> : IAI<TAIContext>
{
  protected TAIContext _context;

  protected AIBase()
  {
    Effects = new();
  }

  public Pipeline Effects { get; }

  public void Update(TAIContext context)
  {
    _context = context;
  }
}
