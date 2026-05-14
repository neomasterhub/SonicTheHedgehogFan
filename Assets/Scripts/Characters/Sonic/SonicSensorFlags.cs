public readonly struct SonicSensorFlags
{
  public static readonly SonicSensorFlags None = new(false, false, false);

  public readonly bool CheckGround;
  public readonly bool CheckCeiling;
  public readonly bool CheckBalancing;

  public SonicSensorFlags(bool checkGround, bool checkCeiling, bool checkBalancing)
  {
    CheckGround = checkGround;
    CheckCeiling = checkCeiling;
    CheckBalancing = checkBalancing;
  }
}
