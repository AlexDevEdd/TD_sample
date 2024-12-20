using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LoadingTree
{
    public sealed class SequenceOperation : ILoadingOperation
    {
        private readonly IReadOnlyList<ILoadingOperation> _operations;

        private readonly float _maxChildWeight;
        private float _accChildWeight;
        private ILoadingOperation _currentOperation;

        private readonly float _weight;

        public SequenceOperation(int weight = 1, params ILoadingOperation[] operations)
        {
            _weight = weight;
            _operations = operations;
            _maxChildWeight = operations.Sum(it => it.GetWeight());
        }

        public async UniTask<LoadingResult> Run(LoadingBundle bundle)
        {
            _currentOperation = null;
            _accChildWeight = 0;

            foreach (ILoadingOperation operation in _operations)
            {
                _currentOperation = operation;

                LoadingResult result = await _currentOperation.Run(bundle);
                if (!result.success)
                    return LoadingResult.Error(result.error);

                _accChildWeight += _currentOperation.GetWeight();
            }

            _currentOperation = null;
            return LoadingResult.Success();
        }
        
        public float GetWeight()
        {
            return _weight;
        }

        public float GetProgress()
        {
            float childWeight = _accChildWeight;

            if (_currentOperation != null) 
                childWeight += _currentOperation.GetWeight() * _currentOperation.GetProgress();

            return childWeight / _maxChildWeight;
        }
        
        [Serializable]
        public sealed class Asset : ILoadingOperationAsset
        {
            [SerializeField]
            private int _weight = 1;
            
            [SerializeReference]
            private ILoadingOperationAsset[] _chidren = default;
            
            public ILoadingOperation Create(DiContainer container)
            {
                IEnumerable<ILoadingOperation> operations = _chidren.Select(it => it.Create(container));
                return new SequenceOperation(_weight, operations.ToArray());
            }
        }
    }
}