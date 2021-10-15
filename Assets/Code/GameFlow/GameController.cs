using Code.Configs;
using Code.Controllers;
using Code.Controllers.Player;
using Code.Views;
using UnityEngine;

namespace Code.GameFlow
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;

        private InputManager _inputManager;
        private Controllers _controllers;

        private PlayerController _playerController;
        
        private SpriteAnimatorConfig _playerAnimatorConfig;
        private SpriteAnimatorController _playerAnimatorController;

        private void Start()
        {
            _inputManager = new InputManager();
            _inputManager.Enable();
            _controllers = new Controllers();

            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimatorController = new SpriteAnimatorController(_playerAnimatorConfig);
            _playerController = new PlayerController(_inputManager, playerView, _playerAnimatorController);


            _controllers.Add(_playerAnimatorController);
            _controllers.Add(_playerController);
            
            _controllers.Init();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Update(deltaTime);
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            _controllers.Update(fixedDeltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Update(deltaTime);
        }

        private void OnDestroy()
        {
            _inputManager.Disable();
            _controllers.Cleanup();
        }
    }
}