using UnityEngine;

namespace CustomPool
{
    public sealed class MonoPool<TContract> : PoolBase<TContract> where TContract : MonoBehaviour
    {
        public MonoPool(TContract prefab, int size, Transform parent) : base(prefab, size, parent) { }
        
        protected override TContract CreateObj()
        {
            return Object.Instantiate(Prefab, Container);
        } 
    }
}