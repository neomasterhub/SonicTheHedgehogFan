using System;

public class PlayerInputSystem
{
  private readonly Func<PlayerInput> _inputSrc;

  private PlayerInput _prev;

  public PlayerInputSystem(Func<PlayerInput> inputSrc)
  {
    _inputSrc = inputSrc;
  }

  public PlayerInput Held { get; private set; }
  public PlayerInput Pressed { get; private set; }
  public PlayerInput Released { get; private set; }

  public void Update()
  {
    _prev = Held;
    Held = _inputSrc();
    Pressed = Held & ~_prev;
    Released = _prev & ~Held;
  }
}
