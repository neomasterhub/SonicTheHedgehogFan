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
  private Vector3 _animalBreakOutOrigin;
  [SerializeField]
  private bool _animalHorizontalDirection;
  [SerializeField]
  [InspectorLabel("Animal")]
  private GameObject _animalObj;
  [SerializeField]
  [InspectorLabel("Animal Prefabs")]
  private GameObject[] _animalPrefabs;
}
