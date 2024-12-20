using CustomPool;
using GameCycle;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private PoolsContainer _poolsContainer;
        
        [SerializeField]
        private int _projectilesPoolSize = 20;
        
        public override void InstallBindings()
        {
            GameCycleInstaller.Install(Container);
            
            BindPoolsContainer();
            ProjectilesInstaller.Install(Container, _projectilesPoolSize);
        }
        
        private void BindPoolsContainer()
        {
            Container.Bind<IPoolsContainer>().To<PoolsContainer>()
                .FromInstance(_poolsContainer)
                .AsSingle()
                .NonLazy();
        }
    }
    
}