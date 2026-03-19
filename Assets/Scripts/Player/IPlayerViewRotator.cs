public interface IPlayerViewRotator
{
  bool Enabled { get; set; }
  float Angle { get; }
  void Rotate(PlayerViewRotatorInput input);
}
