using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AssetManagement
{
    public sealed class PrefabFactoryInjectAsync : IPrefabFactoryAsync
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        
        public PrefabFactoryInjectAsync(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public async UniTask<TComponent> Create<TComponent>(string assetKey)
        {
            var prefab = await _assetProvider.LoadAsync<GameObject>(assetKey);
            var newObject = _instantiator.InstantiatePrefab(prefab);
            return newObject.GetComponent<TComponent>();
        }
    }
}