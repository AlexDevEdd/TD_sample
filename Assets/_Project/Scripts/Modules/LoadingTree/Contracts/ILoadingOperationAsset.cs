using Zenject;

namespace LoadingTree
{
    public interface ILoadingOperationAsset
    {
        ILoadingOperation Create(DiContainer container);
    }
}