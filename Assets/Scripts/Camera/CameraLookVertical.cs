using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachinePositionComposer))]
public class CameraLookVertical : MonoBehaviour
{
  private const float _delay = 2;
  private const float _yLimit = 0.45f;
  private const float _yStep = 0.01f;

  private float _delayTimer;
  private VerticalDirection _prevDirection;
  private CinemachinePositionComposer _camPos;
  private ILookVerticalDirectionProvider _directionProvider;

  public GameObject DirectionProvider;

  private void Awake()
  {
    _camPos = GetComponent<CinemachinePositionComposer>();
    _directionProvider = DirectionProvider.GetComponent<ILookVerticalDirectionProvider>();
  }

  private void FixedUpdate()
  {
    var direction = _directionProvider.LookVerticalDirection;
    var yCurrent = _camPos.Composition.ScreenPosition.y;
    var yTarget = direction switch
    {
      VerticalDirection.Up => _yLimit,
      VerticalDirection.None => 0,
      VerticalDirection.Down => -_yLimit,
      _ => throw direction.ArgumentOutOfRangeException(),
    };

    if (yCurrent == yTarget)
    {
      return;
    }

    if (yCurrent == 0)
    {
      if (_prevDirection == VerticalDirection.None)
      {
        if (direction == VerticalDirection.None)
        {
          _delayTimer = 0;
        }
        else
        {
          _delayTimer = _delay;
        }
      }
      else
      {
        if (direction == VerticalDirection.None)
        {
          _delayTimer = 0;
        }
        else if (direction == _prevDirection)
        {
          _delayTimer -= Time.fixedDeltaTime;
        }
        else
        {
          _delayTimer = _delay;
        }
      }
    }

    _prevDirection = direction;

    if (_delayTimer > 0)
    {
      return;
    }

    _camPos.Composition.ScreenPosition = new(0, Mathf.MoveTowards(yCurrent, yTarget, _yStep));
  }
}
