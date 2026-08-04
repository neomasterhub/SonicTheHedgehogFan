using System;
using UnityEngine;

public class Sound
{
  private readonly float _loopDelay;
  private readonly AudioSource _audioSource;
  private readonly Func<bool> _playCondition;
  private readonly Func<bool> _stopCondition;

  private float _delay;

  public Sound(
    AudioSource audioSource,
    Func<bool> playCondition,
    Func<bool> stopCondition = null,
    float loopDelay = 0)
  {
    _audioSource = audioSource;
    _playCondition = playCondition;
    _stopCondition = stopCondition ?? (() => false);
    _loopDelay = loopDelay;
    _delay = _loopDelay;
  }

  public Sound Play()
  {
    if (_playCondition())
    {
      _audioSource.PlayDelayed(_delay);
      _delay = 0;
    }

    return this;
  }

  public Sound Stop()
  {
    if (_stopCondition())
    {
      _audioSource.Stop();
      _delay = _loopDelay;
    }

    return this;
  }
}
