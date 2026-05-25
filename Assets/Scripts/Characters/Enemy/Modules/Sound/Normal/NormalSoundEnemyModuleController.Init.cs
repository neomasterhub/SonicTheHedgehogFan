using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class NormalSoundEnemyModuleController
{
  protected override void SetSounds()
  {
    if (_attackedAudioClip != null)
    {
      var audioSource = this.AddComponent<AudioSource>();
      audioSource.clip = _attackedAudioClip;
      _sounds.Add(new(
        audioSource,
        () => _context.IsHit && !_context.IsDying));
    }

    if (_dyingAudioClip != null)
    {
      var audioSource = this.AddComponent<AudioSource>();
      audioSource.clip = _dyingAudioClip;
      _sounds.Add(new(
        audioSource,
        () => _context.IsHit && _context.IsDying));
    }
  }
}
