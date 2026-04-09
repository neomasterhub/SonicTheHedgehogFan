using System;

public class PlayerInputSystem
{
  private readonly Func<PlayerInput> _inputSrc;

  private PlayerInput _prevInput;

  public PlayerInputSystem(Func<PlayerInput> inputSrc)
  {
    _inputSrc = inputSrc;
  }

  public PlayerInput Input { get; private set; }

  public void Update()
  {
    _prevInput = Input;
    Input = _inputSrc();
  }

  public bool IsPressed(PlayerInput input)
  {
    return Input.HasFlag(input) && !_prevInput.HasFlag(input);
  }
}
