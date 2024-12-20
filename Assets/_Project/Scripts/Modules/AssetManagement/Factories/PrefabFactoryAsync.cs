using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetManagement
{
    public sealed class PrefabFactoryAsync : IPrefabFactoryAsync
    {
        private readonly IAssetProvider _assetProvider;
        
        public PrefabFactoryAsync(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask<TComponent>Create<TComponent>(string assetKey)
        {
            var prefab = await _assetProvider.LoadAsync<GameObject>(assetKey);
            var newObject  = Object.Instantiate(prefab);
            return newObject.GetComponent<TComponent>();
        }
    }
}