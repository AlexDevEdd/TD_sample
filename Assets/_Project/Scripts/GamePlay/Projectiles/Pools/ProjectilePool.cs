using System;
using AssetManagement;
using CustomPool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public abstract class ProjectilePool : IInitializable, IDisposable, IProjectilePool
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _parent;
        private readonly string _assetKey;
        private readonly int _poolSize;
        
        protected readonly ProjectileBuilder Builder = new();
        protected IMonoPool<Projectile> Pool;
        
        public abstract ProjectileType Type { get;}
        
        protected ProjectilePool(IAssetProvider assetProvider, IPoolsContainer parent, int poolSize, string assetKey)
        {
            _assetProvider = assetProvider;
            _poolSize = poolSize;
            _assetKey = assetKey;
            _parent = parent.Container;
        }

        async void IInitializable.Initialize()
        {
            await CreatePoolsAsync();
        }

        private async UniTask CreatePoolsAsync()
        {
            var projectile = await _assetProvider.LoadPrefabAsync<Projectile>(_assetKey);
            Pool = new MonoPool<Projectile>(projectile, _poolSize, _parent);
            Pool.OnCreatedAction += OnCreatedProjectile;
            Pool.Init().Forget();
        }

        protected abstract void OnCreatedProjectile(Projectile projectile);

        public abstract ProjectileBuilder Get();

        public abstract void Put(Projectile projectile);

        public virtual void Dispose()
        {
            Pool.OnCreatedAction -= OnCreatedProjectile;
        }
    }
}

