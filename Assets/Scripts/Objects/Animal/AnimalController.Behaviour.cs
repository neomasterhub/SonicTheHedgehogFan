/// <summary>
/// Behaviour.
/// </summary>
public partial class AnimalController : IAnimal
{
  public void BreakOut(bool horizontalDirection)
  {
    gameObject.SetActive(true);
  }
}
