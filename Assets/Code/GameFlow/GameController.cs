using Code.Configs;
using Code.Controllers;
using Code.Views;
using UnityEngine;

namespace Code.GameFlow
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        
        private Controllers _controllers;
        
        private SpriteAnimatorConfig _playerAnimatorConfig;
        private SpriteAnimatorController _playerAnimatorController;

        private void Start()
        {
            _controllers = new Controllers();

            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimatorController = new SpriteAnimatorController(_playerAnimatorConfig);

            _playerAnimatorController.StartAnimation(playerView.Renderer, AnimationType.Run, true);

            _controllers.Add(_playerAnimatorController);
            
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
            _controllers.Cleanup();
        }
    }
}