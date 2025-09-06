using System;

[Flags]
public enum SonicState
{
  None = 0,
  Grounded = 1 << 0,
  Falling = 1 << 1,
  Jumping = 1 << 2,
  Crouching = 1 << 3,
}
