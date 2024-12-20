using JetBrains.Annotations;
using Zenject;

namespace LoadingTree
{
    [UsedImplicitly]
    public sealed class LoadingInstaller : Installer<LoadingOperationTree, LoadingInstaller>
    {
        private readonly LoadingOperationTree _loadingTree;

        public LoadingInstaller(LoadingOperationTree loadingTree)
        {
            _loadingTree = loadingTree;
        }
        
        public override void InstallBindings()
        {
            Container
                .Bind<ILoadingOperation>()
                .FromMethod(() => _loadingTree.Create(Container))
                .AsSingle();
        }
    }
}