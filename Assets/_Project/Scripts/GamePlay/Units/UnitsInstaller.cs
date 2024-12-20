using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class UnitsInstaller : MonoInstaller
    {
        [SerializeField]
        private int _poolSize = 10;

        [SerializeField]
        private Transform _targetTransform;
        
        [SerializeField]
        private UnitSpawnPointsHolder _unitSpawnPointsHolder;
        
        [SerializeField]
        private  float _spawnInterval;
       
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitWorld>()
                .AsSingle()
                .WithArguments(_targetTransform)
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UnitPool<Unit>>()
                .AsSingle()
                .WithArguments(_poolSize)
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UnitSpawnSystem>()
                .AsSingle()
                .WithArguments(_spawnInterval)
                .NonLazy();
            
            Container.Bind<UnitSpawnPointsHolder>()
                .FromInstance(_unitSpawnPointsHolder)
                .AsSingle()
                .NonLazy();
        }
    }
}