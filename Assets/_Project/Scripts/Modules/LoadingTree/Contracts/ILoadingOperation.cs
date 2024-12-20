using Cysharp.Threading.Tasks;

namespace LoadingTree
{
    public interface ILoadingOperation
    {
        UniTask<LoadingResult> Run(LoadingBundle bundle);

        float GetWeight() => 1;
        float GetProgress() => 1;
    }
}