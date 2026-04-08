using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly Func<Vector2> _dPadInputSrc;
  private readonly Func<PlayerInput> _buttonInputSrc;

  public PlayerInputSystem(
    Func<Vector2> dPadSrc,
    Func<PlayerInput> buttonInputSrc)
  {
    _dPadInputSrc = dPadSrc;
    _buttonInputSrc = buttonInputSrc;
  }

  public Vector2 DPadInput { get; private set; } = default;
  public PlayerInput ButtonInput { get; private set; }

  public void Update()
  {
    DPadInput = _dPadInputSrc();
    ButtonInput = _buttonInputSrc();
  }
}
