using System;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class TowerAttackBehaviour : IInitializable, ITickable, IDisposable
    {
        private readonly TowerProvider _towerProvider;
        private readonly ITargetable _targetable;
        private readonly IAim _aimBehaviour;
        
        private AttackComponent _attackComponent;
        
        private Unit _currentTarget;
        private float _currenTime;
        private Cycle _cycle;
        
        private CompositeDisposable _disposable;
        
        public TowerAttackBehaviour(TowerProvider towerProvider, ITargetable targetable, IAim aimBehaviour)
        {
            _towerProvider = towerProvider;
            _targetable = targetable;
            _aimBehaviour = aimBehaviour;
        }

        void IInitializable.Initialize()
        {
            _attackComponent = _towerProvider.Value.Get<AttackComponent>();
            
            _disposable = new CompositeDisposable();
            _towerProvider.Value.Get<TargetingComponent>().Target
                .Skip(1)
                .Subscribe(OnTargetChanged)
                .AddTo(_disposable);

            _cycle = new Cycle(_attackComponent.AttackSpeed);
            _cycle.OnCycle += TryFire;
        }

        void ITickable.Tick()
        {
            if (_currentTarget != null)
            {
                 _cycle.Tick(Time.deltaTime);
            }
        }

        private void TryFire()
        {
            if(_targetable.IsLookOnTarget())
                _attackComponent.Fire(_currentTarget.Aim, _aimBehaviour.PredictedTrajectory);
        }

        private void OnTargetChanged(Unit unit)
        {
            if (unit && !_cycle.IsPlaying()) 
                _cycle.Start();
            else
                _cycle.Stop();
            
            _currentTarget = unit;
        }

        void IDisposable.Dispose()
        {
            _cycle.OnCycle -= TryFire;
            _disposable?.Dispose();
        }
    }
}