using Code.Configs;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Updates;

namespace Code.Controllers.Player
{
    public class PlayerMovementController : IUpdatable
    {
        private readonly PlayerView _view;
        private readonly SpriteAnimatorController _spriteAnimator;
        private readonly ContactPool _contactPool;

        private bool _isJumping;
        private bool _isMoving;

        private float _xInputVelocity;
        private float _yVelocity;

        private float _speed = 100.0f;
        private float _jumpSpeed = 7.0f;
        private const float JumpThreshold = 1.0f;
        private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(1, 1, 1);
        
        public PlayerMovementController(PlayerView view, SpriteAnimatorController animatorController)
        {
            _view = view;
            _spriteAnimator = animatorController;
            _spriteAnimator.StartAnimation(view.Renderer, AnimationType.Idle, true);
            _contactPool = new ContactPool(_view.ViewCollider);
        }

        public void Update(float deltaTime)
        {
            _spriteAnimator.Update(deltaTime);
            _contactPool.Update(deltaTime);

            _isMoving = Mathf.Abs(_xInputVelocity) >= float.Epsilon;

            if (_isMoving)
            {
                Move();
            }

            if (_contactPool.IsGrounded)
            {
                _spriteAnimator.StartAnimation(_view.Renderer, _isMoving ? AnimationType.Run : AnimationType.Idle, true);
                if (_isJumping && Mathf.Abs(_view.Rigidbody.velocity.y) <= JumpThreshold)
                {
                    _view.Rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_view.Rigidbody.velocity.y) > JumpThreshold)
                {
                    _spriteAnimator.StartAnimation(_view.Renderer, AnimationType.Jump, true);
                }
            }
        }

        public void SetXVelocity(float xAxis)
        {
            _xInputVelocity = xAxis;
        }

        public void SetJumpState(float value)
        {
            _isJumping = value >= JumpThreshold;
        }

        private void Move()
        {
            var xVelocity = Time.fixedDeltaTime * _speed * _xInputVelocity;
            _view.Rigidbody.velocity = _view.Rigidbody.velocity.With(x: xVelocity);
            _view.transform.localScale = _xInputVelocity < 0 ? _leftScale : _rightScale;
        }
    }
}