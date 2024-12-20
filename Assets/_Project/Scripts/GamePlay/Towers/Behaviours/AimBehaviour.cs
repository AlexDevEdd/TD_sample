using System;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class AimBehaviour : IInitializable, ITickable, IDisposable, IAim
    {
        private const float GRAVITY_ADJUSTMENT_FACTOR = 0.5f;
        
        private readonly TowerProvider _towerProvider;
        private AttackComponent _attackComponent;
        
        private Unit _currentTarget;
        private CompositeDisposable _disposable;
        public Vector3 PredictedTrajectory { get; private set; }

        public AimBehaviour(TowerProvider towerProvider)
        {
            _towerProvider = towerProvider;
        }
        
        void IInitializable.Initialize()
        {
            _attackComponent = _towerProvider.Value.Get<AttackComponent>();
            
            _disposable = new CompositeDisposable();
            _towerProvider.Value.Get<TargetingComponent>().Target
                .Skip(1)
                .Subscribe(OnTargetChanged)
                .AddTo(_disposable);
        }
        
        void ITickable.Tick()
        {
            if (_currentTarget == null) 
                return;
            
            CalculateTrajectory();
        }

        private void CalculateTrajectory()
        {
            PredictedTrajectory = PredictTrajectory(_currentTarget.Aim.position, 
                _currentTarget.Velocity, _attackComponent.FirePoint.position,
                _attackComponent.ProjectileSpeed, _attackComponent.FireMode);
        }
        
        private void OnTargetChanged(Unit unit)
        {
            _currentTarget = unit;
        }
        
        private static Vector3 PredictTrajectory(Vector3 targetPos, Vector3 targetVelocity, 
            Vector3 startPos, float speed, FireMode fireMode)
        {
            if (speed <= 0f)
                return Vector3.zero;
            
            var distanceToTarget = (startPos - targetPos).magnitude;
            var timeToTarget = distanceToTarget / speed;

            var predictedTargetPosition = targetPos + targetVelocity * timeToTarget;
            
            var distance = predictedTargetPosition - startPos;
            var distanceXz = new Vector3(distance.x, 0f, distance.z);
            
            if (fireMode is not FireMode.Parabolic)
                return distanceXz.normalized * speed;
            
            var verticalDistance = distance.y; 
            var horizontalDistance = distanceXz.magnitude;
            
            var estimatedTimeToTarget = horizontalDistance / speed;
            
            var gravityEffect = GRAVITY_ADJUSTMENT_FACTOR 
                                * Mathf.Abs(Physics.gravity.y) 
                                * Mathf.Pow(estimatedTimeToTarget, 2);
            
            var verticalSpeed = (verticalDistance + gravityEffect) / estimatedTimeToTarget;
            
            var result = distanceXz.normalized * speed;
            result.y = verticalSpeed;

            return result;
        }
        
        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
        }
    }
    
}