using UnityEngine;
using static Helpers.Sensors;
using static SharedConsts.Colors;
using static SonicConsts.Sizes;

public class SonicSensorSystem
{
  private static readonly Color _aColor = Color.softGreen;
  private static readonly Color _bColor = Color.green;
  private static readonly Color _cColor = Color.softYellow;
  private static readonly Color _dColor = Color.yellow;
  private static readonly Color _oColor = Color.white;

  private readonly SonicSensorGroup _bigUpSensorGroup;
  private readonly SonicSensorGroup _bigDownSensorGroup;
  private readonly SonicSensorGroup _bigLeftSensorGroup;
  private readonly SonicSensorGroup _bigRightSensorGroup;
  private readonly SonicSensorGroup _smallUpSensorGroup;
  private readonly SonicSensorGroup _smallDownSensorGroup;
  private readonly SonicSensorGroup _smallLeftSensorGroup;
  private readonly SonicSensorGroup _smallRightSensorGroup;

  private Sensor _o;
  private UDFSensor _a;
  private UDFSensor _b;
  private UDFSensor _c;
  private UDFSensor _d;
  private Vector2 _parentPosition;
  private SonicSensorRayLengths? _sensorRayLengths;
  private IMeshRenderer _meshRenderer;

  public SonicSensorSystem(
    SonicSizeMode sizeMode = SonicSizeMode.Big,
    GroundSide groundSide = GroundSide.Down,
    Vector2? parentPosition = null)
  {
    SizeMode = sizeMode;
    GroundSide = groundSide;

    SetSensorGroups(Big.HRadius, Big.VRadius, parentPosition, out _bigDownSensorGroup, out _bigRightSensorGroup, out _bigUpSensorGroup, out _bigLeftSensorGroup);
    SetSensorGroups(Small.HRadius, Small.VRadius, parentPosition, out _smallDownSensorGroup, out _smallRightSensorGroup, out _smallUpSensorGroup, out _smallLeftSensorGroup);

    SetCurrentSensorGroup();
  }

  public SonicSizeMode SizeMode { get; private set; }
  public GroundSide GroundSide { get; private set; }
  public Vector2 ParentPosition => _parentPosition;

  public void Update(SonicSensorContext context)
  {
    if (SizeMode != context.SizeMode || GroundSide != context.GroundSide)
    {
      _sensorRayLengths = null;

      SizeMode = context.SizeMode;
      GroundSide = context.GroundSide;

      SetCurrentSensorGroup();
    }

    SetParentPosition(context.ParentPosition);
    UpdateSensorActiveStates(context.SensorFlags);
    UpdateSensorRayLengths(context.SensorRayLengths);
  }

  public CeilingDetectionResult? DetectCeiling(bool horizontalDirection, LayerMask groundLayer)
  {
    return CDDetectCeiling(_c, _d, groundLayer, horizontalDirection);
  }

  public GroundDetectionResult? DetectGround(bool horizontalDirection, LayerMask groundLayer)
  {
    return ABDetectGround(_a, _b, groundLayer, horizontalDirection, () => IsBalancing(groundLayer));
  }

  public WallDetectionResult? DetectLeftWall(LayerMask groundLayer)
  {
    return DetectWall(_c.FrontRay, _a.FrontRay, groundLayer);
  }

  public WallDetectionResult? DetectRightWall(LayerMask groundLayer)
  {
    return DetectWall(_d.FrontRay, _b.FrontRay, groundLayer);
  }

  public void SetMeshRenderer(IMeshRenderer meshRenderer)
  {
    _meshRenderer = meshRenderer;
    _bigUpSensorGroup.SetMeshRenderer(meshRenderer);
    _bigDownSensorGroup.SetMeshRenderer(meshRenderer);
    _bigLeftSensorGroup.SetMeshRenderer(meshRenderer);
    _bigRightSensorGroup.SetMeshRenderer(meshRenderer);
    _smallUpSensorGroup.SetMeshRenderer(meshRenderer);
    _smallDownSensorGroup.SetMeshRenderer(meshRenderer);
    _smallLeftSensorGroup.SetMeshRenderer(meshRenderer);
    _smallRightSensorGroup.SetMeshRenderer(meshRenderer);
  }

  public void Draw()
  {
    DrawBackground();
    _o.Draw();
    _a.Draw();
    _b.Draw();
    _c.Draw();
    _d.Draw();
  }

  private void DrawBackground()
  {
    var lengths = _sensorRayLengths.Value;
    Vector3[] points = GroundSide switch
    {
      GroundSide.Down => GetBackgroundPoints_DownGround(lengths),
      GroundSide.Right => GetBackgroundPoints_RightGround(lengths),
      GroundSide.Up => GetBackgroundPoints_DownGround(lengths),
      GroundSide.Left => GetBackgroundPoints_LeftGround(lengths),
      _ => throw GroundSide.ArgumentOutOfRangeException(),
    };
    _meshRenderer.DrawPolygon(points, SensorSystemBG);
  }

  private Vector3[] GetBackgroundPoints_DownGround(SonicSensorRayLengths lengths)
  {
    Vector3[] points;

    // Sensors:
    //   C---D
    //   |   |
    //   A-->B
    // UDF:
    //   x   x
    // z-|   |-z
    //   y   y
    if (_o.Enabled)
    {
      var oy = _o.Position.y - lengths.O;
      var dx1 = lengths.BottomUDF.z;
      var dx2 = lengths.TopUDF.z;
      var dy2 = lengths.TopUDF.x;

      points = new Vector3[]
      {
        new(_a.Position.x - dx1, oy),
        new(_b.Position.x + dx1, oy),
        new(_d.Position.x + dx2, _d.Position.y + dy2),
        new(_c.Position.x - dx2, _c.Position.y + dy2),
      };
    }
    else
    {
      var dx1 = lengths.BottomUDF.z;
      var dy1 = lengths.BottomUDF.y;
      var dx2 = lengths.TopUDF.z;
      var dy2 = lengths.TopUDF.x;

      points = new Vector3[]
      {
        new(_a.Position.x - dx1, _a.Position.y - dy1),
        new(_b.Position.x + dx1, _b.Position.y - dy1),
        new(_d.Position.x + dx2, _d.Position.y + dy2),
        new(_c.Position.x - dx2, _c.Position.y + dy2),
      };
    }

    return points;
  }

  private Vector3[] GetBackgroundPoints_RightGround(SonicSensorRayLengths lengths)
  {
    Vector3[] points;

    // Sensors:
    //   D---B
    //   |   |
    //   C-->A
    // UDF:
    //   z
    // x---y
    //
    // x---y
    //   z
    if (_o.Enabled)
    {
      var ox = _o.Position.x + lengths.O;
      var dx1 = lengths.TopUDF.x;
      var dy1 = lengths.TopUDF.z;
      var dy2 = lengths.BottomUDF.z;

      points = new Vector3[]
      {
        new(_c.Position.x - dx1, _c.Position.y - dy1),
        new(ox, _a.Position.y - dy2),
        new(ox, _b.Position.y + dy2),
        new(_d.Position.x - dx1, _d.Position.y + dy1),
      };
    }
    else
    {
      var dx1 = lengths.TopUDF.x;
      var dy1 = lengths.TopUDF.z;
      var dx2 = lengths.BottomUDF.y;
      var dy2 = lengths.BottomUDF.z;

      points = new Vector3[]
      {
        new(_c.Position.x - dx1, _c.Position.y - dy1),
        new(_a.Position.x + dx2, _a.Position.y - dy2),
        new(_b.Position.x + dx2, _b.Position.y + dy2),
        new(_d.Position.x - dx1, _d.Position.y + dy1),
      };
    }

    return points;
  }

  private Vector3[] GetBackgroundPoints_LeftGround(SonicSensorRayLengths lengths)
  {
    Vector3[] points;

    // Sensors:
    //   A---C
    //   |   |
    //   B-->D
    // UDF:
    //   z
    // y---x
    //
    // y---x
    //   z
    if (_o.Enabled)
    {
      var ox = _o.Position.x - lengths.O;
      var dy1 = lengths.BottomUDF.z;
      var dx2 = lengths.TopUDF.x;
      var dy2 = lengths.TopUDF.z;

      points = new Vector3[]
      {
        new(ox, _b.Position.y - dy1),
        new(_d.Position.x + dx2, _d.Position.y - dy2),
        new(_c.Position.x + dx2, _c.Position.y + dy2),
        new(ox, _a.Position.y + dy1),
      };
    }
    else
    {
      var dx1 = lengths.BottomUDF.y;
      var dy1 = lengths.BottomUDF.z;
      var dx2 = lengths.TopUDF.x;
      var dy2 = lengths.TopUDF.z;

      points = new Vector3[]
      {
        new(_b.Position.x - dx1, _b.Position.y - dy1),
        new(_d.Position.x + dx2, _d.Position.y - dy2),
        new(_c.Position.x + dx2, _c.Position.y + dy2),
        new(_a.Position.x - dx1, _a.Position.y + dy1),
      };
    }

    return points;
  }

  private void SetCurrentSensorGroup()
  {
    var currentSensorGroup = SizeMode switch
    {
      SonicSizeMode.Big => GroundSide switch
      {
        GroundSide.Up => _bigUpSensorGroup,
        GroundSide.Down => _bigDownSensorGroup,
        GroundSide.Left => _bigLeftSensorGroup,
        GroundSide.Right => _bigRightSensorGroup,
        _ => throw GroundSide.ArgumentOutOfRangeException(),
      },
      SonicSizeMode.Small => GroundSide switch
      {
        GroundSide.Up => _smallUpSensorGroup,
        GroundSide.Down => _smallDownSensorGroup,
        GroundSide.Left => _smallLeftSensorGroup,
        GroundSide.Right => _smallRightSensorGroup,
        _ => throw GroundSide.ArgumentOutOfRangeException(),
      },
      _ => throw SizeMode.ArgumentOutOfRangeException(),
    };

    _o = currentSensorGroup.O;
    _a = currentSensorGroup.A;
    _b = currentSensorGroup.B;
    _c = currentSensorGroup.C;
    _d = currentSensorGroup.D;
  }

  public void SetParentPosition(Vector2 parentPosition)
  {
    _parentPosition = parentPosition;
    _o.SetParentPosition(parentPosition);
    _a.SetParentPosition(parentPosition);
    _b.SetParentPosition(parentPosition);
    _c.SetParentPosition(parentPosition);
    _d.SetParentPosition(parentPosition);
  }

  public void UpdateSensorActiveStates(SonicSensorFlags flags)
  {
    _o.Enabled = flags.CheckBalancing;

    _c.UpRay.Enabled = flags.CheckCeiling;
    _c.DownRay.Enabled = flags.CheckCeiling;
    _d.UpRay.Enabled = flags.CheckCeiling;
    _d.DownRay.Enabled = flags.CheckCeiling;

    _a.UpRay.Enabled = flags.CheckGround;
    _a.DownRay.Enabled = flags.CheckGround;
    _b.UpRay.Enabled = flags.CheckGround;
    _b.DownRay.Enabled = flags.CheckGround;
  }

  private void UpdateSensorRayLengths(SonicSensorRayLengths lengths)
  {
    if (_sensorRayLengths != lengths)
    {
      _sensorRayLengths = lengths;

      _o.Ray.Length = lengths.O;
      UpdateUDFSensorRayLengths(_c, lengths.TopUDF);
      UpdateUDFSensorRayLengths(_d, lengths.TopUDF);
      UpdateUDFSensorRayLengths(_a, lengths.BottomUDF);
      UpdateUDFSensorRayLengths(_b, lengths.BottomUDF);
    }
  }

  private void UpdateUDFSensorRayLengths(UDFSensor sensor, Vector3 udfLengths)
  {
    sensor.UpRay.Length = udfLengths.x;
    sensor.DownRay.Length = udfLengths.y;
    sensor.FrontRay.Length = udfLengths.z;
  }

  private void SetSensorGroups(
    float hRadius,
    float vRadius,
    Vector2? parentPosition,
    out SonicSensorGroup down,
    out SonicSensorGroup right,
    out SonicSensorGroup up,
    out SonicSensorGroup left)
  {
    down = new(
      a: new(_aColor, new(-hRadius, -vRadius), Vector2.up, Vector2.down, Vector2.left),
      b: new(_bColor, new(hRadius, -vRadius), Vector2.up, Vector2.down, Vector2.right),
      c: new(_cColor, new(-hRadius, vRadius), Vector2.up, Vector2.down, Vector2.left),
      d: new(_dColor, new(hRadius, vRadius), Vector2.up, Vector2.down, Vector2.right),
      o: new(_oColor, Vector2.zero, Vector2.down),
      parentPosition: parentPosition);
    right = new(
      a: new(_aColor, new(vRadius, -hRadius), Vector2.left, Vector2.right, Vector2.down),
      b: new(_bColor, new(vRadius, hRadius), Vector2.left, Vector2.right, Vector2.up),
      c: new(_cColor, new(-vRadius, -hRadius), Vector2.left, Vector2.right, Vector2.down),
      d: new(_dColor, new(-vRadius, hRadius), Vector2.left, Vector2.right, Vector2.up),
      o: new(_oColor, Vector2.zero, Vector2.right),
      parentPosition: parentPosition);
    up = new(
      c: new(_cColor, new(-hRadius, -vRadius), Vector2.down, Vector2.up, Vector2.right),
      d: new(_dColor, new(hRadius, -vRadius), Vector2.down, Vector2.up, Vector2.left),
      a: new(_aColor, new(-hRadius, vRadius), Vector2.down, Vector2.up, Vector2.right),
      b: new(_bColor, new(hRadius, vRadius), Vector2.down, Vector2.up, Vector2.left),
      o: new(_oColor, Vector2.zero, Vector2.up),
      parentPosition: parentPosition);
    left = new(
      c: new(_cColor, new(vRadius, -hRadius), Vector2.right, Vector2.left, Vector2.up),
      d: new(_dColor, new(vRadius, hRadius), Vector2.right, Vector2.left, Vector2.down),
      a: new(_aColor, new(-vRadius, -hRadius), Vector2.right, Vector2.left, Vector2.up),
      b: new(_bColor, new(-vRadius, hRadius), Vector2.right, Vector2.left, Vector2.down),
      o: new(_oColor, Vector2.zero, Vector2.left),
      parentPosition: parentPosition);
  }

  private bool IsBalancing(LayerMask groundLayer)
  {
    return _o.Enabled && !_o.Ray.Cast(groundLayer).HasValue;
  }

  private RaycastHit2D? GetClosestWallHit(SensorRay topRay, SensorRay bottomRay, LayerMask groundLayer)
  {
    var topHit = topRay.Cast(groundLayer);
    var bottomHit = bottomRay.Cast(groundLayer);

    if (topHit == null)
    {
      return bottomHit;
    }

    if (bottomHit == null)
    {
      return topHit;
    }

    return topHit.Value.distance <= bottomHit.Value.distance ? topHit : bottomHit;
  }

  private WallDetectionResult? DetectWall(SensorRay topRay, SensorRay bottomRay, LayerMask groundLayer)
  {
    var hit = GetClosestWallHit(topRay, bottomRay, groundLayer);
    if (hit == null)
    {
      return null;
    }

    return new(hit.Value, topRay.Direction);
  }
}
