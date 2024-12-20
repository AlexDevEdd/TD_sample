using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CustomPool
{
    public abstract class PoolBase<TContract> : IMonoPool<TContract> where TContract : MonoBehaviour
    {
        public event Action<TContract> OnCreatedAction;
        public event Action<TContract> OnSpawnAction;
        public event Action<TContract> OnDeSpawnAction;
        
        private readonly Queue<TContract> _pool = new();
        private readonly HashSet<TContract> _actives = new();
        
        private readonly Transform _parent;
        private readonly int _size;

        protected readonly TContract Prefab;
        protected Transform Container;

        public HashSet<TContract> Actives => _actives;

        protected PoolBase(TContract prefab, int size, Transform parent)
        {
            Prefab = prefab;
            _size = size;
            _parent = parent;
        }

        protected abstract TContract CreateObj();

        public async UniTaskVoid Init()
        {
            for (var i = 0; i < _size; i++)
            {
                if (Container == null)
                {
                    Container = new GameObject($"{Prefab.name}s").transform;
                    Container.SetParent(_parent);
                } 

                var item = CreateObj();
                OnCreated(item);
            }
            
            await UniTask.FromResult(true);
        }
        
        public TContract Spawn()
        {
            if(_pool.TryDequeue(out var item))
            {
                OnSpawn(item);
                return item;
            }

            item = CreateObj();
            OnSpawn(item);
            return item;
        }
        
        public void DeSpawn(TContract item)
        {
            if (_actives.Remove(item))
            {
                item.transform.SetParent(Container);
                item.gameObject.SetActive(false);
                _pool.Enqueue(item);
                OnDeSpawnAction?.Invoke(item);
            }
        }
        
        private void OnCreated(TContract item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
            OnCreatedAction?.Invoke(item);
        } 
        
        private void OnSpawn(TContract item)
        {
            _actives.Add(item);
            item.gameObject.SetActive(true);
            OnSpawnAction?.Invoke(item);
        }
    }
}