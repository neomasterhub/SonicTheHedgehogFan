using System;

[Flags]
public enum SonicState
{
  Airborne = 1 << 0,
  Grounded = 1 << 1,
  Skidding = 1 << 2,
  Balancing = 1 << 3,
  CurlingUp = 1 << 4,
  FallingOffWall = 1 << 5,
}
