using UnityEngine;
using Zenject;

namespace GamePlay
{
    public sealed class EnergyTowerInstaller : MonoInstaller
    {
        [SerializeField] 
        private Entity _entity;

        [SerializeField]
        private AttackData _attackData;
        
        [SerializeField]
        private VerticalYoYoData _verticalYoYoData;
        
        [SerializeField]
        private SphereSensorData _sensorData;
        
        public override void InstallBindings()
        {
            Container.Bind<TowerProvider>()
                .AsSingle()
                .WithArguments(_entity);
            
            Container.BindInterfacesAndSelfTo<AttackComponent>()
                .AsSingle()
                .WithArguments(_attackData);
            
            Container.BindInterfacesAndSelfTo<VerticalYoYoComponent>()
                .AsSingle()
                .WithArguments(_verticalYoYoData);
            
            Container.BindInterfacesAndSelfTo<TargetingComponent>()
                .AsSingle()
                .WithArguments(_sensorData);
            
            EnergyTowerSystemsInstaller.Install(Container);
        }
    }
}