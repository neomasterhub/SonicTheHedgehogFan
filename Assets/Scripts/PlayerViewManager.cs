using UnityEngine;

public class PlayerViewManager
{
  private readonly InputInfo _inputInfo;
  private readonly SpriteRenderer _spriteRenderer;

  public PlayerViewManager(InputInfo inputInfo, SpriteRenderer spriteRenderer)
  {
    _inputInfo = inputInfo;
    _spriteRenderer = spriteRenderer;
  }

  public void Update()
  {
    RotateSprite();
  }

  private void RotateSprite()
  {
    if (_inputInfo.X > 0)
    {
      _spriteRenderer.flipX = false;
    }
    else if (_inputInfo.X < 0)
    {
      _spriteRenderer.flipX = true;
    }
  }
}
