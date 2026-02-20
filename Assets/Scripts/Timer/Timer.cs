using System;

public class Timer
{
  public Timer(float durationSeconds)
  {
    DurationSeconds = durationSeconds;
  }

  private event Action Started;
  private event Action Completed;

  public float DurationSeconds { get; }
  public float RemainingSeconds { get; private set; }
  public bool IsRunning { get; private set; }
  public bool IsCompleted => RemainingSeconds <= 0;

  public Timer WhenStarted(Action action)
  {
    Started += action;
    return this;
  }

  public Timer WhenCompleted(Action action)
  {
    Completed += action;
    return this;
  }

  public void Start()
  {
    RemainingSeconds = DurationSeconds;
    IsRunning = true;
    Started?.Invoke();
  }

  public void Update(float deltaTime)
  {
    if (!IsRunning)
    {
      return;
    }

    RemainingSeconds -= deltaTime;

    if (RemainingSeconds <= 0)
    {
      RemainingSeconds = 0f;
      IsRunning = false;
      Completed?.Invoke();
    }
  }
}
