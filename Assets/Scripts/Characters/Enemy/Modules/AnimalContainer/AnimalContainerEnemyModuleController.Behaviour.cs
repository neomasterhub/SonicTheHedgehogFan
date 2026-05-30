/// <summary>
/// Behaviour.
/// </summary>
public partial class AnimalContainerEnemyModuleController
{
  public override void Apply()
  {
    ReleaseAnimal();
  }

  private void ReleaseAnimal()
  {
    if (!_isAnimalReleased && _context.IsDying)
    {
      _isAnimalReleased = true;

      _animal.BreakOut(transform.position, _animalHorizontalDirection);
    }
  }
}
