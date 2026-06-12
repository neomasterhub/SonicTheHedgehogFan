/// <summary>
/// Pipeline.
/// </summary>
public partial class BlockController
{
  private void FixedUpdate()
  {
    SetContact();
  }

  private void SetContact()
  {
    if (_player.ContactLeftWallTransform == transform
      || _player.ContactRightWallTransform == transform)
    {
      _player.ContactBlock = this;
    }
  }
}
