using System;
using AssetManagement;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using LoadingTree;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace App
{
    [UsedImplicitly]
    public sealed class LoadSceneOperation : ILoadingOperation
    {
        private AsyncOperationHandle<SceneInstance> _operation;
        
        public float GetProgress()
        {
            return _operation.PercentComplete;
        }
        
        public float GetWeight()
        {
            return 50;
        }

        public async UniTask<LoadingResult> Run(LoadingBundle bundle)
        {
            if (!bundle.TryGet(LoadingBundleKeys.ASSET_PROVIDER, out IAssetProvider assetProvider))
                return LoadingResult.Error("ASSET_PROVIDER doesn't exist!");
            
            try
            {
                await assetProvider.InitializeAsync();
                _operation = Addressables.LoadSceneAsync(AssetKeys.GAME);
                await _operation;
            }
            catch (Exception)
            {
                return LoadingResult.Error($"Can't load scene {AssetKeys.GAME}!");
            }

            return LoadingResult.Success();
        }
        
        [UsedImplicitly]
        public sealed class Asset : LoadingOperationAsset<LoadSceneOperation> { }
    }
}