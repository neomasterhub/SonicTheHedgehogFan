using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class SpinSmokeController
{
  private void FixedUpdate()
  {
    UpdateVisibility();
  }

  private void UpdateVisibility()
  {
    var psm = _particleSystem.main;
    var color = psm.startColor.color;

    if (_source.SpinSmoke == this)
    {
      if (color.a == _alphaMax)
      {
        return;
      }

      color.a = Mathf.Min(_alphaMax, color.a + _alphaInc);
      psm.startColor = new ParticleSystem.MinMaxGradient(color);
    }
    else
    {
      if (color.a == 0)
      {
        return;
      }

      color.a = Mathf.Max(0, color.a - _alphaDec);
      psm.startColor = new ParticleSystem.MinMaxGradient(color);
    }
  }
}
