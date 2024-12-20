using Cysharp.Threading.Tasks;
using GameCycle;
using JetBrains.Annotations;
using LoadingTree;
using Zenject;

namespace App
{
    [UsedImplicitly]
    public sealed class StartGameOperation : ILoadingOperation
    {
        public float GetProgress()
        {
            return 2;
        }
        
        public UniTask<LoadingResult> Run(LoadingBundle bundle)
        {
            DiContainer gameContainer = bundle.Get<DiContainer>(LoadingBundleKeys.DI_CONTAINER);
            gameContainer.Resolve<GameCycleSystem>().OnStartEvent();
            
            return UniTask.FromResult(LoadingResult.Success());
        }
        
        [UsedImplicitly]
        public sealed class Asset : LoadingOperationAsset<StartGameOperation> { }
    }
}