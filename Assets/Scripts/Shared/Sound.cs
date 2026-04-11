using System;
using UnityEngine;

public class Sound
{
  private readonly AudioSource _audioSource;
  private readonly Func<bool> _playCondition;
  private readonly Func<bool> _stopCondition;

  public Sound(AudioSource audioSource, Func<bool> playCondition, Func<bool> stopCondition)
  {
    _audioSource = audioSource;
    _playCondition = playCondition;
    _stopCondition = stopCondition;
  }

  public void Play()
  {
    if (_playCondition())
    {
      _audioSource.Play();
    }
  }

  public void Stop()
  {
    if (_stopCondition())
    {
      _audioSource.Stop();
    }
  }
}
