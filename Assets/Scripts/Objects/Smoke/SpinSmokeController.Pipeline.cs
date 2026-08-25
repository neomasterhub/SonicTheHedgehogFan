using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class SpinSmokeController
{
  private void FixedUpdate()
  {
    BeginFrame();
    UpdateVisibility();
    Destroy();
  }

  private void BeginFrame()
  {
    _timerSystem.Update(Time.fixedDeltaTime);
  }

  private void UpdateVisibility()
  {
    var psm = _particleSystem.main;
    _color = psm.startColor.color;

    if (_source.SpinSmoke == this)
    {
      if (_color.a == _alphaMax)
      {
        return;
      }

      _color.a = Mathf.Min(_alphaMax, _color.a + _alphaInc);
      psm.startColor = new ParticleSystem.MinMaxGradient(_color);
    }
    else
    {
      if (_color.a == 0)
      {
        return;
      }

      _color.a = Mathf.Max(0, _color.a - _alphaDec);
      psm.startColor = new ParticleSystem.MinMaxGradient(_color);
    }
  }

  private void Destroy()
  {
    if (_color.a == 0 && _source.SpinSmoke != this)
    {
      _timerSystem.StartIfNotRunning(_destroyTimer);
    }
  }
}
