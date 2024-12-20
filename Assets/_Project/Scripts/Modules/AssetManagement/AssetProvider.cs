using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AssetManagement
{
    [UsedImplicitly]
    public sealed class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetRequests = new ();

        public async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync()
                .ToUniTask();
        }

        public async UniTask<TAsset> LoadAsync<TAsset>(string key) where TAsset : class
        {
            if (!_assetRequests.TryGetValue(key, out var handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _assetRequests.Add(key, handle);
            }

            await handle.ToUniTask();
            
            return handle.Result as TAsset;
        }

        public async UniTask<TAsset> LoadAsync<TAsset>(AssetReference assetReference) where TAsset : class
        {
            return await LoadAsync<TAsset>(assetReference.AssetGUID);
        }
        
        public async UniTask<TComponent>LoadPrefabAsync<TComponent>(string assetKey) where TComponent : Component
        {
            var prefab = await LoadAsync<GameObject>(assetKey);
            return prefab.GetComponent<TComponent>();
        }
        
        public async UniTask<SceneInstance> LoadSceneAsync(string sceneName, LoadSceneMode mode)
        {
            var handle = Addressables.LoadSceneAsync(sceneName, mode);
            _assetRequests.TryAdd(sceneName, handle);
            return await handle.ToUniTask();
        }
        
        public async UniTask UnLoadSceneAsync(string assetKey)
        {
            if(_assetRequests.TryGetValue(assetKey, out var handle))
            {
                await Addressables.UnloadSceneAsync(handle);
                _assetRequests.Remove(assetKey);
            }
        }
        
        public async UniTask<List<string>> GetAssetsListAsync<TAsset>(string key)
        {
            return await GetAssetsListAsync(key, typeof(TAsset));
        }

        public async UniTask<List<string>> GetAssetsListAsync(string key, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(key, type);

            var locations = await operationHandle.ToUniTask();

            var assetKeys = new List<string>(locations.Count);

            foreach (var location in locations) 
                assetKeys.Add(location.PrimaryKey);
            
            Addressables.Release(operationHandle);
            return assetKeys;
        }

        public async UniTask<TAsset[]> LoadAllAsync<TAsset>(List<string> keys) where TAsset : class
        {
            var tasks = new List<UniTask<TAsset>>(keys.Count);

            foreach (var key in keys) 
                tasks.Add(LoadAsync<TAsset>(key));

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask WarmupAssetsAsync(string key)
        {
            var assetsList = await GetAssetsListAsync(key);
            await LoadAllAsync<object>(assetsList);
        }

        public async UniTask ReleaseAssetsAsync(string label)
        {
            var assetsList = await GetAssetsListAsync(label);
            
            foreach (var assetKey in assetsList)
                if (_assetRequests.TryGetValue(assetKey, out var handler))
                {
                    Addressables.Release(handler);
                    _assetRequests.Remove(assetKey);
                }
        }

        public void Cleanup()
        {
            foreach (var assetRequest in _assetRequests) 
                Addressables.Release(assetRequest.Value);
            
            _assetRequests.Clear();
        }

        public void PrintAssets()
        {
            foreach (var handle in _assetRequests)
            {
                Debug.Log($"{handle.Value.DebugName}");
            }
        }
    }
}