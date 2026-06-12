using UnityEngine;

public interface IBlockPlayer
{
  Transform ContactWallTransform { get; }
  IBlock ContactBlock { set; }
}
