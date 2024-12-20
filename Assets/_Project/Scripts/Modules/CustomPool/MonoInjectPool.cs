using UnityEngine;
using Zenject;

namespace CustomPool
{
    public sealed class MonoInjectPool<TContract> : PoolBase<TContract> where TContract : MonoBehaviour
    {
        private readonly IInstantiator _instantiator;
        
        public MonoInjectPool(IInstantiator instantiator, TContract prefab, int size, Transform parent)
            : base(prefab, size, parent)
        {
            _instantiator = instantiator;
            Init().Forget();
        }
        
        protected override TContract CreateObj()
        {
            var newObject = _instantiator.InstantiatePrefab(Prefab, Container);
            return newObject.GetComponent<TContract>();
        } 
    }
}