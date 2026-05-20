public interface INextUpdater<TNext>
{
  void SetNext(TNext next);
  void UpdateNext();
}
