public interface ISystemModule<TSystemContext>
{
  void Initialize(TSystemContext context);
  void Apply();
}
