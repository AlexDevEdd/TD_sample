using AssetManagement;
using JetBrains.Annotations;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class ProjectilesInstaller : Installer<int, ProjectilesInstaller>
    {
        private readonly int _projectilesPoolSize;

        public ProjectilesInstaller(int projectilesPoolSize)
        {
            _projectilesPoolSize = projectilesPoolSize;
        }
        
        public override void InstallBindings()
        {
            BindProjectilesFactory();
            BindCannonProjectilesPool();
            BindEnergyProjectilesPool();
        }

        private void BindEnergyProjectilesPool()
        {
            Container.BindInterfacesAndSelfTo<EnergyProjectilePool>()
                .AsSingle()
                .WithArguments(_projectilesPoolSize, AssetKeys.ENERGY_PROJECTILE)
                .NonLazy();
        }
        private void BindCannonProjectilesPool()
        {
            Container.BindInterfacesAndSelfTo<CannonProjectilePool>()
                .AsSingle()
                .WithArguments(_projectilesPoolSize, AssetKeys.CANNON_PROJECTILE)
                .NonLazy();
        }

        private void BindProjectilesFactory()
        {
            Container.Bind<IProjectileFactory>().To<ProjectileFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}