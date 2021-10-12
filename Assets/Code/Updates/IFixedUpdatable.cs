namespace Updates
{
    public interface IFixedUpdatable : IController
    {
        public void FixedUpdate(float fixedDeltaTime);
    }
}
