using System;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class TargetableBehaviour : IInitializable, IDisposable, ITargetable
    {
        private readonly TowerProvider _towerProvider;
        
        private CompositeDisposable _disposable;
        private Unit _currentTarget;
        
        public TargetableBehaviour(TowerProvider towerProvider)
        {
            _towerProvider = towerProvider;
        }
        
        void IInitializable.Initialize()
        {
            _disposable = new CompositeDisposable();
            _towerProvider.Value.Get<TargetingComponent>().Target
                .Skip(1)
                .Subscribe(OnTargetChanged)
                .AddTo(_disposable);
        }

        public bool IsLookOnTarget()
        {
            return _currentTarget != null;
        }

        private void OnTargetChanged(Unit unit)
        {
            _currentTarget = unit;
        }
        
        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
        }
    }
}