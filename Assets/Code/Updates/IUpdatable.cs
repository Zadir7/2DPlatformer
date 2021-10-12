namespace Updates
{
    public interface IUpdatable : IController
    {
        public void Update(float deltaTime);
    }
}
