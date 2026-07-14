using UnityEngine;

public static class ZoneConsts
{
  public static class Colors
  {
    public static readonly Color Death;

    static Colors()
    {
      Death = Color.firebrick;
    }
  }

  public static class Drawing
  {
    public const float ZoneEdgeWidth = 0.2f;
  }
}
