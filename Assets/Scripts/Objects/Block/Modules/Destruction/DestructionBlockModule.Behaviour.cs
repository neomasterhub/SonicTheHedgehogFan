using static BlockConsts;
using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class DestructionBlockModule
{
  public override void Apply()
  {
    ToggleBlockLayer();
  }

  private void ToggleBlockLayer()
  {
    if (_player.ContactBlock != null
      || !_player.IsRolling)
    {
      gameObject.layer = BlockLayerIndex;
      return;
    }

    var pSpeedX = _player.SpeedX;

    if (_player.IsGrounded
      && (pSpeedX > MinDestructionPlayerSpeedX
      || pSpeedX < -MinDestructionPlayerSpeedX))
    {
      gameObject.layer = 0;
      return;
    }

    gameObject.layer = BlockLayerIndex;
  }
}
