using static BlockConsts;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class GroundSpeedBlockModuleController
{
  public GroundSpeedBlockModuleController()
    : base()
  {
    _gravitySpeedSpx = GravitySpeedSpx;
    _maxFallSpeedPx = MaxFallSpeedPx;
    _minPushUpSpeedPx = MinPushUpSpeedPx;
    _maxPushUpSpeedPx = MaxPushUpSpeedPx;
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);

    _gravitySpeed = _gravitySpeedSpx / SpxPerUnit;
    _maxFallSpeed = _maxFallSpeedPx / PxPerUnit;
    _minPushUpSpeed = _minPushUpSpeedPx / PxPerUnit;
    _maxPushUpSpeed = _maxPushUpSpeedPx / PxPerUnit;

    _player = _context.Player;
  }
}
