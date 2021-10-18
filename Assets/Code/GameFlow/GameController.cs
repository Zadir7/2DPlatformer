using System;
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
        [SerializeField] private LevelEndView levelEndView;
        public event Action OnLevelEnd = () => { };

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

            
            SubscribeEvents();

            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimatorController = new SpriteAnimatorController(_playerAnimatorConfig);
            _playerController = new PlayerController(_inputManager, playerView, _playerAnimatorController);


            _controllers.Add(_playerAnimatorController);
            _controllers.Add(_playerController);
            
            _controllers.Init();
        }

        private void SubscribeEvents()
        {
            levelEndView.OnLevelEndEnter += LevelEndEnter;
            OnLevelEnd += EndLevel;
        }

        private void UnsubscribeEvents()
        {
            levelEndView.OnLevelEndEnter -= LevelEndEnter;
            OnLevelEnd -= EndLevel;
        }

        private void LevelEndEnter(Collider2D objCollider)
        {
            if (objCollider == playerView.ViewCollider)
            {
                OnLevelEnd.Invoke();
            }
        }

        private void EndLevel()
        {
            Debug.Log("Level finished");
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
            UnsubscribeEvents();
            _inputManager.Disable();
            _controllers.Cleanup();
        }
    }
}