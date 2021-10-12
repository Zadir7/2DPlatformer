using System;
using UnityEngine;

namespace GameFlow
{
    internal class GameController : MonoBehaviour
    {
        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
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