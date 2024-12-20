using AssetManagement;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    [UsedImplicitly]
    public sealed class SceneLoader : ISceneLoader
    {
        private readonly IAssetProvider _assetProvider;
        private string _currentSceneName;

        public SceneLoader(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask LoadAsync(string assetKey, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (_currentSceneName == assetKey)
                await UniTask.CompletedTask;
            
            await _assetProvider.LoadSceneAsync(assetKey, mode);
        }
        
        public async UniTask UnLoadAsync(string assetKey)
        {
            await _assetProvider.UnLoadSceneAsync(assetKey);
        }
    }
}