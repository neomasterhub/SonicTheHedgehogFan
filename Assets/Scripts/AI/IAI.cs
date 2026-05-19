public interface IAI<TAIContext>
{
  Pipeline Effects { get; }
  void SetContext(TAIContext context);
}
