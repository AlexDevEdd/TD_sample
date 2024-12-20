using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using LoadingTree;
using UnityEngine;
using Zenject;

namespace App
{
    [UsedImplicitly]
    public sealed class LoadSceneContextOperation : ILoadingOperation
    {
        public float GetProgress()
        {
            return 2;
        }
        
        public UniTask<LoadingResult> Run(LoadingBundle bundle)
        {
            var sceneContext = Object.FindObjectOfType<SceneContext>();
            if (!sceneContext)
                LoadingResult.Error("Scene context is null!");

            DiContainer container = sceneContext.Container;
            bundle.Add(LoadingBundleKeys.DI_CONTAINER, container);

            return UniTask.FromResult(LoadingResult.Success());
        }
        
        [UsedImplicitly]
        public sealed class Asset : LoadingOperationAsset<LoadSceneContextOperation> { }
    }
}