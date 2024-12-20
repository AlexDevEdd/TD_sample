using JetBrains.Annotations;
using Zenject;

namespace GameCycle
{
    [UsedImplicitly]
    public class GameCycleInstaller : Installer<GameCycleInstaller>
    {
        public override void InstallBindings()
        {
            BindGameCycleSystem();
        }
        
        private void BindGameCycleSystem()
        {
            Container.BindInterfacesAndSelfTo<GameCycleSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}