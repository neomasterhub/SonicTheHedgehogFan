using UnityEngine;

public class Blinker
{
  private readonly SpriteRenderer _spriteRenderer;

  private bool _dimmed;
  private float _alpha;
  private float _time;
  private float _timer;
  private float _interval;

  public Blinker(SpriteRenderer spriteRenderer)
  {
    _spriteRenderer = spriteRenderer;
  }

  public void Run(float alpha, float timer, float interval)
  {
    _dimmed = true;
    _alpha = alpha;

    _time = 0;
    _timer = timer;
    _interval = interval;
  }
}
