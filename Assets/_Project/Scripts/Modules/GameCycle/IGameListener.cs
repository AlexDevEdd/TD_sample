namespace GameCycle
{
    public interface IGameListener { }
    
    public interface IGameFinish : IGameListener
    {
        void OnFinish();
    }
    
    public interface IGamePause : IGameListener
    {
        void OnPause();
    }
    
    public interface IGameResume : IGameListener
    {
        void OnResume();
    }
    
    public interface IGameStart : IGameListener
    {
        void OnStart();
    }
    
    public interface ITick : IGameListener
    {
        void Tick(float delta);
    }
    
    public interface IFixedTick : IGameListener
    {
        void FixedTick(float fixedDelta);
    }
    
    public interface ILateTick : IGameListener
    {
        void LateTick(float delta);
    }
}