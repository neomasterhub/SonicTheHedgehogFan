using System;
using Neomaster.RingBuffer;
using static SharedConsts.Input;

public class PlayerInputSystem
{
  private readonly Func<PlayerInput> _inputSrc;
  private readonly RingBuffer<PlayerInput> _pressedHistory;

  private PlayerInput _prev;

  public PlayerInputSystem(Func<PlayerInput> inputSrc)
  {
    _inputSrc = inputSrc;
    _pressedHistory = new(PressedHistoryCapacity);
  }

  public float X { get; private set; }
  public float Y { get; private set; }
  public PlayerInput Held { get; private set; }
  public PlayerInput Pressed { get; private set; }
  public PlayerInput Released { get; private set; }

  public void Update()
  {
    _prev = Held;

    Held = _inputSrc();
    Pressed = Held & ~_prev;
    Released = _prev & ~Held;

    if (Pressed != PlayerInput.None)
    {
      _pressedHistory.Push(Pressed);
    }

    SetDPad();
  }

  private void SetDPad()
  {
    X = 0;
    Y = 0;

    var upHeld = Held.HasAny(PlayerInput.Up);
    var downHeld = Held.HasAny(PlayerInput.Down);
    var leftHeld = Held.HasAny(PlayerInput.Left);
    var rightHeld = Held.HasAny(PlayerInput.Right);

    if (leftHeld && !rightHeld)
    {
      X = -1;
    }
    else if (!leftHeld && rightHeld)
    {
      X = 1;
    }

    if (upHeld && !downHeld)
    {
      Y = 1;
    }
    else if (!upHeld && downHeld)
    {
      Y = -1;
    }
  }
}
