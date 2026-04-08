using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly Func<Vector2> _dPadSrc;
  private readonly Func<ButtonInput> _buttonInputSrc;

  public PlayerInputSystem(Func<Vector2> dPadSrc, Func<ButtonInput> buttonInputSrc)
  {
    _dPadSrc = dPadSrc;
    _buttonInputSrc = buttonInputSrc;
  }

  public Vector2 DPad { get; private set; }
  public ButtonInput ButtonInput { get; private set; }

  public void Update()
  {
    DPad = _dPadSrc();
    ButtonInput = _buttonInputSrc();
  }
}
