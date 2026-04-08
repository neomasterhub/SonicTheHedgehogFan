using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly Func<Vector2> _dPadInputSrc;
  private readonly Func<PlayerInput> _inputSrc;

  public PlayerInputSystem(
    Func<Vector2> dPadSrc,
    Func<PlayerInput> inputSrc)
  {
    _dPadInputSrc = dPadSrc;
    _inputSrc = inputSrc;
  }

  public Vector2 DPadInput { get; private set; } = default;
  public PlayerInput Input { get; private set; }

  public void Update()
  {
    DPadInput = _dPadInputSrc();
    Input = _inputSrc();
  }
}
