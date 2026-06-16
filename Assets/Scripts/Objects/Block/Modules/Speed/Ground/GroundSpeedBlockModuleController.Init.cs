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
    _gravitySpeed = _gravitySpeedSpx / SpxPerUnit;
    _maxFallSpeed = _maxFallSpeedPx / PxPerUnit;
  }
}
