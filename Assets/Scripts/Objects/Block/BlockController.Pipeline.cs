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
    if (_player.ContactWallTransform == transform)
    {
      _player.ContactBlock = this;
    }
  }
}
