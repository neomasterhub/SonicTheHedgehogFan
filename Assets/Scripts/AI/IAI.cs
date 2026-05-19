public interface IAI<TAIContext>
{
  Pipeline Effects { get; }
  void Update(TAIContext context);
}
