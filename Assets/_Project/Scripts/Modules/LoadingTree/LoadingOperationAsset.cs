using Zenject;

namespace LoadingTree
{
    public class LoadingOperationAsset<T> : ILoadingOperationAsset where T : ILoadingOperation
    {
        public ILoadingOperation Create(DiContainer container)
        {
            return container.Instantiate<T>();
        }
    }
}