using JetBrains.Annotations;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class CannonTowerSystemsInstaller : Installer<CannonTowerSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<AimBehaviour>()
                .AsCached();
            
            Container
                .BindInterfacesAndSelfTo<TowerRotateBehaviour>()
                .AsCached(); 
            
            Container
                .BindInterfacesAndSelfTo<TowerAttackBehaviour>()
                .AsCached();
        }
    }
}