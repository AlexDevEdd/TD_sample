using UnityEngine;

namespace CustomPool
{
    public sealed class WorldSpaceContainer : MonoBehaviour, IPoolsContainer
    {
        [SerializeField]
        private Canvas _canvas;

        public Transform Container => transform;
        
        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

    }
}