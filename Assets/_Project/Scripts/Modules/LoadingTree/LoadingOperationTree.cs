using UnityEngine;
using Zenject;

namespace LoadingTree
{
    [CreateAssetMenu(
        fileName = "LoadingOperationTree",
        menuName = "Loading/New LoadingOperationTree"
    )]
    public sealed class LoadingOperationTree : ScriptableObject, ILoadingOperationAsset
    {
        [SerializeReference]
        private ILoadingOperationAsset _root;
        
        public ILoadingOperation Create(DiContainer container)
        {
            return _root.Create(container);
        }
    }
}