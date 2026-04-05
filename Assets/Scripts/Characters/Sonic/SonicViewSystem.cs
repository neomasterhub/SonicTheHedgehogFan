using UnityEngine;

public class SonicViewSystem
{
  private readonly Animator _animator;
  private readonly PlayerInputSystem _inputSystem;
  private readonly SpriteRenderer _spriteRenderer;

  private SonicViewContext _context;

  public SonicViewSystem(Animator animator, PlayerInputSystem inputSystem, SpriteRenderer spriteRenderer)
  {
    _animator = animator;
    _inputSystem = inputSystem;
    _spriteRenderer = spriteRenderer;
  }

  public void Update(SonicViewContext context)
  {
    _context = context;
  }
}
