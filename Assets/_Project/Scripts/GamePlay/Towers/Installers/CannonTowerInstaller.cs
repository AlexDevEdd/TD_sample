using UnityEngine;
using Zenject;

namespace GamePlay
{
    public sealed class CannonTowerInstaller : MonoInstaller
    {
        [SerializeField] 
        private Entity _entity;

        [SerializeField]
        private AttackData _attackData;
        
        [SerializeField]
        private HorizontalRotationData _horizontalData;
        
        [SerializeField]
        private VerticalRotationData _verticalData;
        
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
            
            Container.BindInterfacesAndSelfTo<HorizontalRotationComponent>()
                .AsSingle()
                .WithArguments(_horizontalData);
            
            Container.BindInterfacesAndSelfTo<VerticalRotationComponent>()
                .AsSingle()
                .WithArguments(_verticalData);
            
            Container.BindInterfacesAndSelfTo<TargetingComponent>()
                .AsSingle()
                .WithArguments(_sensorData);
            
            CannonTowerSystemsInstaller.Install(Container);
        }
    }
}