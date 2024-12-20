using System;
using GameCycle;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class TowerRotateBehaviour : IInitializable, IGameStart, ITick, IDisposable, ITargetable
    {
        private readonly TowerProvider _towerProvider;
        private readonly IAim _aim;
        
        private VerticalRotationComponent _verticalRotation;
        private HorizontalRotationComponent _horizontalRotation;
        
        private Unit _currentTarget;
        private CompositeDisposable _disposable;
        
        public TowerRotateBehaviour(TowerProvider towerProvider, IAim aim)
        {
            _towerProvider = towerProvider;
            _aim = aim;
        }

        void IInitializable.Initialize()
        {
            _verticalRotation = _towerProvider.Value.Get<VerticalRotationComponent>();
            _horizontalRotation = _towerProvider.Value.Get<HorizontalRotationComponent>();
            
            _disposable = new CompositeDisposable();
            _towerProvider.Value.Get<TargetingComponent>().Target
                .Skip(1)
                .Subscribe(OnTargetChanged)
                .AddTo(_disposable);
        }
        
        public bool IsLookOnTarget()
        {
            return _horizontalRotation.IsLookOnTarget();
        }

        void ITick.Tick(float delta)
        {
            Rotate();
        }

        private void Rotate()
        {
            var deltaTime = Time.deltaTime;
            if (_currentTarget == null)
            {
                DefaultRotation(deltaTime);
                return;
            }
            
            if (_aim.PredictedTrajectory == Vector3.zero)
            {
                DefaultRotation(deltaTime);
                return;
            }
            
            _verticalRotation.Rotate(_aim.PredictedTrajectory, deltaTime);
            _horizontalRotation.Rotate(_aim.PredictedTrajectory, deltaTime);
        }

        private void DefaultRotation(float deltaTime)
        {
            _verticalRotation.ResetRotation(deltaTime);
            _horizontalRotation.ResetRotation(deltaTime);
        }

        private void OnTargetChanged(Unit unit)
        {
            _currentTarget = unit;
        }

        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
        }

        public void OnStart()
        {
            Debug.Log("START");
        }
    }
}