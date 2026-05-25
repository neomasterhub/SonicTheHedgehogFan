using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class DeathDefeatAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private Timer _dyingTimer;

  [SerializeField]
  private float _dyingDuration;
}
