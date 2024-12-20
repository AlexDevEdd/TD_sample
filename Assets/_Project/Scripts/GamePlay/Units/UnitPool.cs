using AssetManagement;
using CustomPool;
using Cysharp.Threading.Tasks;
using GameCycle;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public interface IUnitPool<T> where T : MonoBehaviour
    {
        public T Get();
        public void Put(T unit);
    }

    [UsedImplicitly]
    public class UnitPool<T> : IInitializable, IUnitPool<T> where T : MonoBehaviour
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _parent;
        private readonly int _poolSize;
        
        private IMonoPool<T> _pool;
        
        
        public UnitPool(IAssetProvider assetProvider, IPoolsContainer parent, int poolSize)
        {
            _assetProvider = assetProvider;
            _poolSize = poolSize;
            _parent = parent.Container;
        }

        public async void Initialize()
        {
            var prefab = await _assetProvider.LoadPrefabAsync<T>(AssetKeys.UNIT);
            _pool = new MonoPool<T>(prefab, _poolSize, _parent);
            _pool.Init().Forget();
        }
        
        public T Get()
        {
            var unit = _pool.Spawn();
            return unit;
        }

        public void Put(T unit)
        {
            _pool.DeSpawn(unit);
        }
    }
}