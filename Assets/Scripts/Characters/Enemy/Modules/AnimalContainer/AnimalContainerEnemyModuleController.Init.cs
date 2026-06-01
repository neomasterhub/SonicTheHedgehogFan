using UnityEngine;
using static EnemyConsts.Physics;
using static SharedConsts.Utilities;

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
    if (_animalObj == null)
    {
      var animalPrefab = _animalPrefabs[SystemRandom.Next(0, _animalPrefabs.Length)];
      animalPrefab.SetActive(false);

      _animalObj = Instantiate(animalPrefab, transform.position + _animalBreakOutOrigin, Quaternion.identity);
    }

    _animal = _animalObj.GetComponent<IAnimal>();
  }
}
