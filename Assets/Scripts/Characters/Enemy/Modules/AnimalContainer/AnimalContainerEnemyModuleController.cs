using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class AnimalContainerEnemyModuleController
  : EnemyModuleControllerBase
{
  private bool _isAnimalReleased;
  private IAnimal _animal;

  [SerializeField]
  [InspectorLabel("Animal")]
  private GameObject _animalObj;
  [SerializeField]
  private Vector3 _animalBreakOutOrigin;
  [SerializeField]
  private bool _animalHorizontalDirection;
}
