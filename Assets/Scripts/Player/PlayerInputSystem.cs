using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly Func<Vector2> _dPadSrc;

  public PlayerInputSystem(Func<Vector2> dPadSrc)
  {
    _dPadSrc = dPadSrc;
  }

  public Vector2 DPad { get; private set; }

  public void Update()
  {
    DPad = _dPadSrc();
  }
}
