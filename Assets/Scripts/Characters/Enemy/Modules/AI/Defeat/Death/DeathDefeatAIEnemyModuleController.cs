using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class DeathDefeatAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private Timer _deadActiveTimer;

  [SerializeField]
  private float _deadActiveDuration;
}
