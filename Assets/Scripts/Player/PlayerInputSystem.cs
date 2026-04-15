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

  public string GetPressedHistory()
  {
    var history = new char[PressedHistoryCapacity];
    var j = 0;

    for (var i = 0; i < _pressedHistory.Right.Length; i++, j++)
    {
      history[j] = _pressedHistory.Right[i].ToChar();
    }

    for (var i = 0; i < _pressedHistory.Left.Length; i++, j++)
    {
      history[j] = _pressedHistory.Left[i].ToChar();
    }

    return new string(history);
  }

  public bool CheckLastPressed(PlayerInput[] input)
  {
    return _pressedHistory.EndsWith(input);
  }

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
