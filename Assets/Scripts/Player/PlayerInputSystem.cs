using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly int _inputHistoryLimit;
  private readonly InputState[] _inputHistory;
  private readonly Func<Vector2> _dPadSrc;
  private readonly Func<ButtonInput> _buttonInputSrc;

  private int _inputHistoryIndex = 0;

  public PlayerInputSystem(
    Func<Vector2> dPadSrc,
    Func<ButtonInput> buttonInputSrc,
    int inputHistoryLimit = 10)
  {
    _dPadSrc = dPadSrc;
    _buttonInputSrc = buttonInputSrc;

    _inputHistoryLimit = inputHistoryLimit;
    _inputHistory = new InputState[inputHistoryLimit];
  }

  public Vector2 DPad { get; private set; }
  public ButtonInput ButtonInput { get; private set; }

  public void Update()
  {
    DPad = _dPadSrc();
    ButtonInput = _buttonInputSrc();

    _inputHistory[_inputHistoryIndex] = new(DPad, ButtonInput);
    _inputHistoryIndex = (_inputHistoryIndex + 1) % _inputHistoryLimit;
  }
}
