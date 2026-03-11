using System;

[Flags]
public enum PlayerState
{
  None = 0,
  Airborne = 1 << 0,
  Grounded = 1 << 1,
  Skidding = 1 << 2,
}
