namespace GamePlay
{
    public interface IEntity
    {
        T Get<T>();
        T Get<T>(object id);
        
        bool TryGet<T>(out T component) where T : class;
        bool TryGet<T>(object id, out T component) where T : class;
    }
}