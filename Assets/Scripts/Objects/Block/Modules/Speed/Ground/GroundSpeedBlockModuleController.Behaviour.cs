using System;
using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class GroundSpeedBlockModuleController
{
  public override void Apply()
  {
    SetSpeed();
    EndFrame();
  }

  private void SetSpeed()
  {
    if (_player.IsRolling
      && !_player.IsGrounded
      && _player.SpeedY > 0
      && _player.SpeedY > _context.SpeedY
      && _player.PositionY > _context.PositionY - _player.VRadius - _context.VRadius - _clearance
      && Mathf.Abs(_player.PositionX - _context.PositionX) < _context.HRadius)
    {
      SetSpeed_PushedUp();
      return;
    }

    if (_context.Ground == null)
    {
      SetSpeed_Airborne();
    }
    else
    {
      SetSpeed_Grounded();
    }
  }

  private void SetSpeed_PushedUp()
  {
    _isPushedUp = true;
    _player.IsStoppedByCeiling = true;
    _context.IsPushedUpIntersecting = true;

    _context.SpeedY = Mathf.Max(_player.SpeedY, _minPushUpSpeed);
  }

  private void SetSpeed_Airborne()
  {
    if (_fallAfterPushedUp && !_isPushedUp)
    {
      return;
    }

    _context.SpeedX = _context.Speed;
    _context.SpeedY -= _gravitySpeed;

    if (_context.SpeedY < -_maxFallSpeed)
    {
      _context.SpeedY = -_maxFallSpeed;
    }
  }

  private void SetSpeed_Grounded()
  {
    if (_context.Speed == 0)
    {
      _context.SpeedX = 0;
      _context.SpeedY = 0;

      return;
    }

    var angleRad = _context.Ground.Value.AngleRad;
    _context.SpeedX = _context.Speed * MathF.Cos(angleRad);
    _context.SpeedY = _context.Speed * MathF.Sin(angleRad);
  }

  private void EndFrame()
  {
    if (_context.IsPushedUpIntersecting)
    {
      _context.IsPushedUpIntersecting = _player.PositionY > _context.PositionY - _player.VRadius - _context.VRadius - _clearance;
    }
  }
}
