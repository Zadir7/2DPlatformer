using Code.Configs;
using Code.Utils;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Player
{
    public class PlayerMovementController
    {
        private readonly PlayerView _view;
        private readonly SpriteAnimatorController _spriteAnimator;

        private bool _isJumping;
        private bool _isMoving;

        private float _xVelocity;
        private float _yVelocity;
        private float _groundLevel;
        private float _deltaTime;

        private float _speed = 3.0f;
        private float _jumpSpeed = 9.0f;
        private const float Gravity = -9.8f;
        private const float JumpThreshold = 1.0f;
        private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(1, 1, 1);

        public bool IsGrounded => _view.transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;

        public PlayerMovementController(PlayerView view, SpriteAnimatorController animatorController)
        {
            _view = view;
            _spriteAnimator = animatorController;
            _spriteAnimator.StartAnimation(view.Renderer, AnimationType.Idle, true);

            _groundLevel = view.transform.position.y;
        }

        public void Update(float deltaTime)
        {
            _deltaTime = deltaTime;
            _spriteAnimator.Update(deltaTime);

            _isMoving = Mathf.Abs(_xVelocity) >= float.Epsilon;

            if (_isMoving)
            {
                Move();
            }

            if (IsGrounded)
            {
                _spriteAnimator.StartAnimation(_view.Renderer, _isMoving ? AnimationType.Run : AnimationType.Idle, true);
                if (_isJumping && _yVelocity <= 0)
                {
                    _yVelocity = _jumpSpeed;
                } else if (_yVelocity < 0)
                {
                    _yVelocity = float.Epsilon;
                    _view.transform.position = _view.transform.position.With(y: _groundLevel);
                }
            }
            else
            {
                _yVelocity += Gravity * _deltaTime;
                if (_isJumping)
                {
                    _spriteAnimator.StartAnimation(_view.Renderer, AnimationType.Jump, true);
                }
                _view.transform.position += Vector3.up * (_yVelocity * _deltaTime);
            }
        }

        public void SetXVelocity(float xAxis)
        {
            _xVelocity = xAxis;
        }

        public void SetJumpState(float value)
        {
            _isJumping = value >= JumpThreshold;
        }

        private void Move()
        {
            var transform = _view.transform;
            transform.position += Vector3.right * (_deltaTime * _speed * _xVelocity);
            transform.localScale = _xVelocity < 0 ? _leftScale : _rightScale;
        }
    }
}