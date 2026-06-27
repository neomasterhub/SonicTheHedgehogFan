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
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);

    _context.FallAfterPushedUp = _fallAfterPushedUp;

    _gravitySpeed = _gravitySpeedSpx / SpxPerUnit;
    _maxFallSpeed = _maxFallSpeedPx / PxPerUnit;
    _pushUpSpeed = _pushUpSpeedPx / PxPerUnit;

    _player = _context.Player;
  }
}
