using System;
using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class GroundSpeedBlockModuleController
{
  public override void Apply()
  {
    SetPushedUpIntersecting();
    SetSpeed();
  }

  private void SetPushedUpIntersecting()
  {
    _prevIsPushedUpIntersecting = _context.IsPushedUpIntersecting;

    var yp = _player.PositionY + _player.VRadius;
    var y0 = _context.PositionY - _context.VRadius;
    var y1 = y0 + _hitboxVRadius;
    var y2 = y0 - _hitboxVRadius;

    if (_context.IsPushedUpIntersecting)
    {
      if (_context.IsDestroyed
        || !_player.IsRolling
        || _player.IsGrounded
        || yp > y1
        || yp < y2
        || Mathf.Abs(_player.PositionX - _context.PositionX) > _context.HRadius)
      {
        _context.IsPushedUpIntersecting = false;
      }
    }
    else
    {
      if (_player.IsRolling
        && !_player.IsGrounded
        && _player.SpeedY > 0
        && _player.SpeedY > _context.SpeedY
        && yp < y1
        && yp > y2
        && Mathf.Abs(_player.PositionX - _context.PositionX) < _context.HRadius)
      {
        _context.IsPushedUpIntersecting = true;
      }
    }
  }

  private void SetSpeed()
  {
    if (_context.IsPushedUpIntersecting
      && !_prevIsPushedUpIntersecting)
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
    _context.SpeedY = _pushUpSpeed;
  }

  private void SetSpeed_Airborne()
  {
    if (_context.FallAfterPushedUp && !_isPushedUp)
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
}
