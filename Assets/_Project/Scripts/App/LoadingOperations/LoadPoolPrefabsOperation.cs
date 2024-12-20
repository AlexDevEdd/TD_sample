using System;
using AssetManagement;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using LoadingTree;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace App
{
    [UsedImplicitly]
    public sealed class LoadPoolPrefabsOperation : ILoadingOperation
    {
        private AsyncOperationHandle<GameObject> _operation;

        public float GetProgress()
        {
            return 2;
        }

        public async UniTask<LoadingResult> Run(LoadingBundle bundle)
        {
            if (!bundle.TryGet(LoadingBundleKeys.ASSET_PROVIDER, out IAssetProvider assetProvider))
                return LoadingResult.Error("ASSET_PROVIDER doesn't exist!");
            
            try
            {
                await assetProvider.LoadAsync<GameObject>(AssetKeys.UNIT);
                await assetProvider.LoadAsync<GameObject>(AssetKeys.CANNON_PROJECTILE);
                await assetProvider.LoadAsync<GameObject>(AssetKeys.ENERGY_PROJECTILE);
            }
            catch (Exception)
            {
                return LoadingResult.Error("Can't load pool prefabs!");
            }

            return LoadingResult.Success();
        }
        
        [UsedImplicitly]
        public sealed class Asset : LoadingOperationAsset<LoadPoolPrefabsOperation> { }
    }
}