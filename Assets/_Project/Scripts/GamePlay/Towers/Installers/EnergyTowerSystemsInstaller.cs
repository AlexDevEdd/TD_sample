using JetBrains.Annotations;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class EnergyTowerSystemsInstaller : Installer<EnergyTowerSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<AimBehaviour>()
                .AsCached();
            
            Container
                .BindInterfacesAndSelfTo<TargetableBehaviour>()
                .AsCached(); 
            
            Container
                .BindInterfacesAndSelfTo<TowerAttackBehaviour>()
                .AsCached();
        }
    }
}