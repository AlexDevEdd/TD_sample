using AssetManagement;
using CustomPool;
using JetBrains.Annotations;

namespace GamePlay
{
    [UsedImplicitly]
    public class CannonProjectilePool : ProjectilePool
    {
        public override ProjectileType Type => ProjectileType.Cannon;

        public CannonProjectilePool(IAssetProvider assetProvider, IPoolsContainer parent, int poolSize, string assetKey)
            : base(assetProvider, parent, poolSize, assetKey) { }

        protected override void OnCreatedProjectile(Projectile projectile)
        {
            Builder
                .SetProjectile(projectile)
                .IsUpdatable(false)
                .SetMoveStrategy(new RigidbodyMoveStrategy())
                .Build();
        }

        public override ProjectileBuilder Get()
        {
            var projectile = Pool.Spawn();
            projectile.OnDead += Put;
            return Builder.SetProjectile(projectile);;
        }

        public override void Put(Projectile projectile)
        {
            projectile.OnDead -= Put;
            Pool.DeSpawn(projectile);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            foreach (var projectile in Pool.Actives) 
                projectile.OnDead -= Put;
        }
    }
}