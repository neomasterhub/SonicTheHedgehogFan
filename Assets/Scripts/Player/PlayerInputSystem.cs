using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly Func<Vector2> _dPadSrc;
  private readonly Func<Vector3> _abcSrc;

  public PlayerInputSystem(Func<Vector2> dPadSrc, Func<Vector3> abcSrc)
  {
    _dPadSrc = dPadSrc;
    _abcSrc = abcSrc;
  }

  public Vector2 DPad { get; private set; }
  public Vector3 ABC { get; private set; }

  public void Update()
  {
    DPad = _dPadSrc();
    ABC = _abcSrc();
  }
}
