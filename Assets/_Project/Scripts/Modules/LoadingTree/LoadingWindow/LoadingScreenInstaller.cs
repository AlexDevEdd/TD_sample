using UnityEngine;
using Zenject;

namespace LoadingTree
{
    [CreateAssetMenu(
        fileName = "LoadingScreenInstaller",
        menuName = "Loading/New LoadingScreenInstaller"
    )]
    public sealed class LoadingScreenInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private LoadingScreen _screenPrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<LoadingScreen>()
                .FromComponentInNewPrefab(_screenPrefab)
                .AsSingle()
                .OnInstantiated<LoadingScreen>((_, it) => it.Hide())
                .NonLazy();
        }
    }
}