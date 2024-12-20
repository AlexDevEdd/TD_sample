using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AssetManagement
{
    public interface IAssetProvider
    {
        public UniTask InitializeAsync();
        public UniTask<TAsset> LoadAsync<TAsset>(AssetReference assetReference) where TAsset : class;
        public UniTask<TAsset> LoadAsync<TAsset>(string key) where TAsset : class;
        public UniTask<TComponent> LoadPrefabAsync<TComponent>(string assetKey) where TComponent : Component;
        public UniTask<SceneInstance> LoadSceneAsync(string assetKey, LoadSceneMode mode);
        public UniTask UnLoadSceneAsync(string assetKey);
        public UniTask<List<string>> GetAssetsListAsync<TAsset>(string key);
        public UniTask<List<string>> GetAssetsListAsync(string key, Type type = null);
        public UniTask<TAsset[]> LoadAllAsync<TAsset>(List<string> keys) where TAsset : class;
        public UniTask WarmupAssetsAsync(string key);
        public UniTask ReleaseAssetsAsync(string label);
        public void Cleanup();
        public void PrintAssets();
    }
}