using System;

[Flags]
public enum SonicState
{
  Airborne = 1 << 0,
  Grounded = 1 << 1,
  Skidding = 1 << 2,
  Balancing = 1 << 3,
  FallingOffWall = 1 << 4,
}
