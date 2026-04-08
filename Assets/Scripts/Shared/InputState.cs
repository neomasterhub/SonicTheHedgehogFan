using UnityEngine;

public readonly struct InputState
{
  public readonly Vector2 DPad;
  public readonly ButtonInput ButtonInput;

  public InputState(Vector2 dPad, ButtonInput buttonInput)
  {
    DPad = dPad;
    ButtonInput = buttonInput;
  }
}
