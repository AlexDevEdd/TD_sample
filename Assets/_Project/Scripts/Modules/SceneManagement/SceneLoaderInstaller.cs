using JetBrains.Annotations;
using Zenject;

namespace SceneManagement
{
    [UsedImplicitly]
    public sealed class SceneLoaderInstaller : Installer<SceneLoaderInstaller>
    {
        public override void InstallBindings()
        {
            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}