/// <summary>
/// Pipeline.
/// </summary>
public partial class SonicController
{
  private void FixedUpdate()
  {
    Update();
    Output();
  }

  private void Update()
  {
    _prevState = _state;
    _prevGroundSide = _groundSide;
    _prevIsGrounded = _isGrounded;
  }
}
