using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class NormalSoundEnemyModuleController
  : SoundEnemyModuleControllerBase
{
  [SerializeField]
  [InspectorLabel("Attacked")]
  private AudioClip _attackedAudioClip;

  [SerializeField]
  [InspectorLabel("Dying")]
  private AudioClip _dyingAudioClip;
}
