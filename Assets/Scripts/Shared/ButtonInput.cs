using System;

[Flags]
public enum ButtonInput
{
  None = 0,
  Start = 1,
  A = 1 << 1,
  B = 1 << 2,
  C = 1 << 3,
  X = 1 << 4,
  Y = 1 << 5,
  Z = 1 << 6,
}
