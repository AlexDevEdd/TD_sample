using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public interface ISceneLoader
    {
        public UniTask LoadAsync(string assetKey, LoadSceneMode mode = LoadSceneMode.Single);
        public UniTask UnLoadAsync(string assetKey);
    }
}