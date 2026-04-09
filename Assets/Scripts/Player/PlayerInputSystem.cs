using System;

public class PlayerInputSystem
{
  private readonly Func<PlayerInput> _inputSrc;

  private PlayerInput _previous;

  public PlayerInputSystem(Func<PlayerInput> inputSrc)
  {
    _inputSrc = inputSrc;
  }

  public PlayerInput Current { get; private set; }
  public PlayerInput Pressed { get; private set; }

  public void Update()
  {
    _previous = Current;
    Current = _inputSrc();
    Pressed = Current & ~_previous;
  }
}
