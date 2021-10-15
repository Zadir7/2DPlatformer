using Code.Views;
using UnityEngine.InputSystem;
using Updates;

namespace Code.Controllers.Player
{
    public class PlayerController : IInitializable, IUpdatable, ICleanup
    {
        private readonly InputManager _input;
        private readonly PlayerMovementController _movement;

        public PlayerController(InputManager input, PlayerView view, SpriteAnimatorController spriteAnimator)
        {
            _input = input;
            _movement = new PlayerMovementController(view, spriteAnimator);
        }
        
        public void Init()
        {
            _input.General.Movement.started += ReadXAxisInput;
            _input.General.Jump.started += JumpIsPressed;
            _input.General.Movement.canceled += ReadXAxisInput;
            _input.General.Jump.canceled += JumpIsPressed;
        }

        public void Update(float deltaTime)
        {
            _movement.Update(deltaTime);
        }

        public void Cleanup()
        {
            _input.General.Movement.performed -= ReadXAxisInput;
            _input.General.Movement.canceled -= ReadXAxisInput;
            _input.General.Jump.performed -= JumpIsPressed;
            _input.General.Jump.canceled -= JumpIsPressed;
        }

        private void ReadXAxisInput(InputAction.CallbackContext context) => _movement.SetXVelocity(context.ReadValue<float>());
        private void JumpIsPressed(InputAction.CallbackContext context) => _movement.SetJumpState(context.ReadValue<float>());
    }
}