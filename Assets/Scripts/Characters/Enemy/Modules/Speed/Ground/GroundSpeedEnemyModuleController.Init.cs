using static EnemyConsts.Physics;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class GroundSpeedEnemyModuleController
{
  public GroundSpeedEnemyModuleController()
    : base()
  {
    _gravitySpeedSpx = GravitySpeedSpx;
    _maxFallSpeedPx = MaxFallSpeedPx;
  }

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    _gravitySpeed = _gravitySpeedSpx / SpxPerUnit;
    _maxFallSpeed = _maxFallSpeedPx / PxPerUnit;
  }
}
