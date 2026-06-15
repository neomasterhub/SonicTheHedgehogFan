/// <summary>
/// Pipeline.
/// </summary>
public partial class BlockController
{
  private void FixedUpdate()
  {
    SetContact();
    ApplyMovement();
    ApplyModules();
  }

  private void SetContact()
  {
    if (_player.ContactLeftWallTransform == transform
      || _player.ContactRightWallTransform == transform)
    {
      _player.ContactBlock = this;
    }
  }

  private void ApplyMovement()
  {
    if (_player.ContactBlock == this
      && _player.IsPushing)
    {
      transform.position += new UnityEngine.Vector3(_player.SpeedX, 0);
    }
  }

  private void ApplyModules()
  {
    for (var i = 0; i < _modules.Length; i++)
    {
      _modules[i].Apply();
    }
  }
}
