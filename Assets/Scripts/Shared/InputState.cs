using UnityEngine;

public readonly struct InputState
{
  public readonly Vector2 DPad;
  public readonly PlayerInput ButtonInput;

  public InputState(Vector2 dPad, PlayerInput buttonInput)
  {
    DPad = dPad;
    ButtonInput = buttonInput;
  }
}
