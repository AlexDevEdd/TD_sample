using System.Collections.Generic;
using AssetManagement;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Zenject;

namespace LoadingTree
{
    [UsedImplicitly]
    public sealed class GameLauncher : IInitializable
    {
        private readonly ILoadingOperation _operation;
        private readonly AssetProvider _assetProvider;
        private readonly LoadingScreen _loadingScreen;
        private bool _isLoading;

        public GameLauncher(AssetProvider assetProvider, ILoadingOperation operation, LoadingScreen loadingScreen)
        {
            _assetProvider = assetProvider;
            _operation = operation;
            _loadingScreen = loadingScreen;
        }

        void IInitializable.Initialize()
        {
            Launch();
        }

        public void Launch()
        {
            LoadInternal().Forget();
            UpdateProgress().Forget();
        }

        private async UniTaskVoid LoadInternal()
        {
            _isLoading = true;
            _loadingScreen.Show();

            var bundle = new LoadingBundle(
            
               new KeyValuePair<string, object>(LoadingBundleKeys.ASSET_PROVIDER, _assetProvider)
            );

            await _operation.Run(bundle);

            _loadingScreen.Hide();
            _isLoading = false;
        }

        private async UniTaskVoid UpdateProgress()
        {
            while (_isLoading)
            {
                float progress = _operation.GetProgress();
                _loadingScreen.SetProgress(progress);
                await UniTask.Yield();
            }
        }
    }
}