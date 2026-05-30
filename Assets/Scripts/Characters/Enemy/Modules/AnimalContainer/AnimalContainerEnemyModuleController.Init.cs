using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class AnimalContainerEnemyModuleController
{
  public AnimalContainerEnemyModuleController()
  {
    _animalBreakOutOrigin = AnimalBreakOutOrigin;
  }

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
