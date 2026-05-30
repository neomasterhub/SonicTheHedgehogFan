/// <summary>
/// Init.
/// </summary>
public partial class AnimalContainerEnemyModuleController
{
  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _animal = _animalObj.GetComponent<IAnimal>();
  }
}
