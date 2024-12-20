using AssetManagement;
using LoadingTree;
using SceneManagement;
using UnityEngine;
using Zenject;

namespace App
{
    [CreateAssetMenu(
        fileName = "ApplicationInstaller",
        menuName = "Loading/New ApplicationInstaller"
    )]
    public sealed class ApplicationInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private LoadingOperationTree _loadingTree;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameLauncher>()
                .AsSingle();
            
            AssetProviderInstaller.Install(Container);
            SceneLoaderInstaller.Install(Container);
            LoadingInstaller.Install(Container, _loadingTree);
        }
    }
}