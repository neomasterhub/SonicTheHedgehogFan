using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachinePositionComposer))]
public class CameraLookVertical : MonoBehaviour
{
  private const float _delay = 2;
  private const float _yMax = 0.45f;
  private const float _yMin = -0.5f;
  private const float _yStep = 0.01f;

  private float _yTarget;
  private float _yCurrent;
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

  private void LateUpdate()
  {
    var direction = _directionProvider.LookVerticalDirection;
    _yCurrent = _camPos.Composition.ScreenPosition.y;
    _yTarget = direction switch
    {
      VerticalDirection.Up => _yMax,
      VerticalDirection.None => 0,
      VerticalDirection.Down => _yMin,
      _ => throw direction.ArgumentOutOfRangeException(),
    };

    if (_yCurrent == 0)
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
          _delayTimer -= Time.deltaTime;
        }
        else
        {
          _delayTimer = _delay;
        }
      }
    }

    _prevDirection = direction;

    if (_delayTimer <= 0)
    {
      _camPos.Composition.ScreenPosition = new(0, Mathf.MoveTowards(_yCurrent, _yTarget, _yStep));
    }
  }
}
