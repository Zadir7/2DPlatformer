namespace Updates
{
    public interface ILateUpdatable : IController
    {
        public void LateUpdate(float deltaTime);
    }
}
