using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LoadingTree
{
    public sealed class ParallelOperation : ILoadingOperation
    {
        private readonly IReadOnlyList<ILoadingOperation> _operations;

        private readonly float _weight;
        private readonly float _maxChildWeight;

        public ParallelOperation(int weight = 1, params ILoadingOperation[] operations)
        {
            _weight = weight;
            _operations = operations;
            _maxChildWeight = operations.Sum(it => it.GetWeight());
        }

        public async UniTask<LoadingResult> Run(LoadingBundle bundle)
        {
            int count = _operations.Count;
            var pool = System.Buffers.ArrayPool<UniTask<LoadingResult>>.Shared;
            UniTask<LoadingResult>[] tasks = pool.Rent(count);

            try
            {
                for (int i = 0; i < count; i++)
                {
                    ILoadingOperation operation = _operations[i];
                    UniTask<LoadingResult> task = operation.Run(bundle);
                    tasks[i] = task;
                }

                LoadingResult[] results = await UniTask.WhenAll(tasks);
              
                for (int i = 0; i < count; i++)
                {
                    LoadingResult result = results[i];
                    if (!result.success)
                        return LoadingResult.Error(result.error);
                }

                return LoadingResult.Success();
            }
            finally
            {
                pool.Return(tasks);
            }
        }
        
        public float GetWeight()
        {
            return _weight;
        }

        public float GetProgress()
        {
            float currentWeight = 0;

            foreach (ILoadingOperation operation in _operations)
                currentWeight += operation.GetWeight() * operation.GetProgress();

            return currentWeight / _maxChildWeight;
        }
        
        [Serializable]
        public sealed class Asset : ILoadingOperationAsset
        {
            [SerializeField]
            private int _weight = 1;
            
            [SerializeReference]
            private ILoadingOperationAsset[] _children = default;
            
            public ILoadingOperation Create(DiContainer container)
            {
                IEnumerable<ILoadingOperation> operations = _children.Select(it => it.Create(container));
                return new ParallelOperation(_weight, operations.ToArray());
            }
        }
    }
}