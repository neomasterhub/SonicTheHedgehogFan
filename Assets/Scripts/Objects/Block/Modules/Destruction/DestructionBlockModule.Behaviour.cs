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
    if (_player.ContactBlock != null)
    {
      return;
    }

    if (_player.IsRolling
      && (_player.SpeedX > 0.02f
      || _player.SpeedX < -0.02f))
    {
      gameObject.layer = 0;
    }
    else
    {
      gameObject.layer = 6;
    }
  }
}
