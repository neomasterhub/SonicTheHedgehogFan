using System;

[Flags]
public enum ButtonInput
{
  None = 0,
  Start = 1,
  Left = 1 << 1,
  Right = 1 << 2,
  Up = 1 << 3,
  Down = 1 << 4,
  A = 1 << 5,
  B = 1 << 6,
  C = 1 << 7,
  X = 1 << 8,
  Y = 1 << 9,
  Z = 1 << 10,
}
