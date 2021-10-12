using System.Collections.Generic;
using Updates;

namespace Code.GameFlow
{
    internal class Controllers : IInitializable, IUpdatable, IFixedUpdatable, ILateUpdatable, ICleanup
    {
        private readonly List<IInitializable> _initializedControllers;
        private readonly List<IUpdatable> _updatableControllers;
        private readonly List<IFixedUpdatable> _fixedUpdatableControllers;
        private readonly List<ILateUpdatable> _lateUpdatableControllers;
        private readonly List<ICleanup> _cleanupControllers;

        internal Controllers()
        {
            _initializedControllers = new List<IInitializable>();
            _updatableControllers = new List<IUpdatable>();
            _fixedUpdatableControllers = new List<IFixedUpdatable>();
            _lateUpdatableControllers = new List<ILateUpdatable>();
            _cleanupControllers = new List<ICleanup>();
        }

        internal Controllers Add(IController controller)
        {
            if (controller is IInitializable initializedController)
            {
                _initializedControllers.Add(initializedController);
            }

            if (controller is IUpdatable updatableController)
            {
                _updatableControllers.Add(updatableController);
            }

            if (controller is IFixedUpdatable fixedUpdatableController)
            {
                _fixedUpdatableControllers.Add(fixedUpdatableController);
            }

            if (controller is ILateUpdatable lateUpdatableController)
            {
                _lateUpdatableControllers.Add(lateUpdatableController);
            }

            if (controller is ICleanup cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }
            return this;
        }
        
        public void Init()
        {
            foreach (var controller in _initializedControllers)
            {
                controller.Init();
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var controller in _updatableControllers)
            {
                controller.Update(deltaTime);
            }
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var controller in _fixedUpdatableControllers)
            {
                controller.FixedUpdate(fixedDeltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            foreach (var controller in _lateUpdatableControllers)
            {
                controller.LateUpdate(deltaTime);
            }
        }

        public void Cleanup()
        {
            foreach (var controller in _cleanupControllers)
            {
                controller.Cleanup();
            }
        }
    }
}