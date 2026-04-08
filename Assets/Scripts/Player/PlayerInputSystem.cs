using System;
using UnityEngine;

public class PlayerInputSystem
{
  private readonly int _inputHistoryLimit;
  private readonly InputState[] _inputHistory;
  private readonly Func<Vector2> _dPadInputSrc;
  private readonly Func<ButtonInput> _buttonInputSrc;

  private int _inputHistoryIndex = 0;

  public PlayerInputSystem(
    Func<Vector2> dPadSrc,
    Func<ButtonInput> buttonInputSrc,
    int inputHistoryLimit = 10)
  {
    _dPadInputSrc = dPadSrc;
    _buttonInputSrc = buttonInputSrc;

    _inputHistoryLimit = inputHistoryLimit;
    _inputHistory = new InputState[inputHistoryLimit];
  }

  public Vector2 DPadInput { get; private set; } = default;
  public ButtonInput ButtonInput { get; private set; }

  public void Update()
  {
    DPadInput = _dPadInputSrc();
    ButtonInput = _buttonInputSrc();

    Debug.Log($"ButtonInput: {ButtonInput}");

    _inputHistory[_inputHistoryIndex] = new(DPadInput, ButtonInput);
    _inputHistoryIndex = (_inputHistoryIndex + 1) % _inputHistoryLimit;
  }
}
