using UnityEngine;

public interface IBlockPlayer
{
  Transform ContactLeftWallTransform { get; }
  Transform ContactRightWallTransform { get; }
  IBlock ContactBlock { set; }
}
