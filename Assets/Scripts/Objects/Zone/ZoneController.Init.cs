using System;
using UnityEngine;
using static SharedConsts;
using static ZoneConsts.Drawing;
using ZoneColors = ZoneConsts.Colors;

/// <summary>
/// Init.
/// </summary>
public partial class ZoneController
{
  private void Awake()
  {
    InitializeComponents();
    InitializeDrawing();
  }

  private void InitializeComponents()
  {
    if (_zoneObjectObj == null)
    {
      _zoneObjectObj = GameObject.FindWithTag(Tags.Player);
    }

    _zoneObjectCollider = _zoneObjectObj.GetComponent<Collider2D>();
    _zoneObject = _zoneObjectObj.GetComponent<IZoneObject>();
    _zoneCollider = GetComponent<Collider2D>();
  }

  private void InitializeDrawing()
  {
    var meshRenderer = GameObject.Find("Mesh Renderer").GetComponent<IMeshRenderer>();

    var color = Color.magenta;

    if (_zoneType.HasAny(ZoneType.Death))
    {
      color = ZoneColors.Death;
    }

    switch (_zoneCollider)
    {
      case EdgeCollider2D ec:
        InitializeDrawing_Edge(meshRenderer, ec, color);
        break;

      default:
        throw new NotSupportedException($"Collider type '{_zoneCollider.GetType().Name}' is not supported.");
    }
  }

  public void InitializeDrawing_Edge(IMeshRenderer meshRenderer, EdgeCollider2D ec, Color color)
  {
    _drawZoneCollider = () =>
    {
      var points = ec.points;
      var transform = ec.transform;

      for (var i = 0; i < points.Length - 1; i++)
      {
        meshRenderer.DrawLine(
          transform.TransformPoint(points[i]),
          transform.TransformPoint(points[i + 1]),
          ZoneEdgeWidth,
          color);
      }
    };
  }
}
