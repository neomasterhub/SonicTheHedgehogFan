using UnityEngine;

public class AlphaBlinker
{
  private readonly SpriteRenderer _spriteRenderer;

  private bool _dimmed;
  private bool _isRunning;
  private float _time;
  private float _timer;
  private float _interval;
  private Color _dimmedColor;
  private Color _sourceColor;

  public AlphaBlinker(SpriteRenderer spriteRenderer)
  {
    _spriteRenderer = spriteRenderer;
  }

  public bool IsRunning => _isRunning;

  public void Start(float alpha, float timer, float interval)
  {
    _isRunning = true;

    _time = 0;
    _timer = timer;
    _interval = interval;

    _sourceColor = _spriteRenderer.color;
    _dimmedColor = _sourceColor;
    _dimmedColor.a = alpha;

    ToggleAlpha();
  }

  public void Stop()
  {
    _time = 0;
    _timer = 0;
    _interval = 0;
    _isRunning = false;

    if (_dimmed)
    {
      ToggleAlpha();
    }
  }

  public void Update(float deltaTime)
  {
    if (!_isRunning)
    {
      return;
    }

    if (_timer <= 0)
    {
      Stop();
      return;
    }

    _time += deltaTime;
    _timer -= deltaTime;

    if (_time >= _interval)
    {
      _time = 0;
      ToggleAlpha();
    }
  }

  private void ToggleAlpha()
  {
    _dimmed = !_dimmed;
    _spriteRenderer.color = _dimmed ? _dimmedColor : _sourceColor;
  }
}
