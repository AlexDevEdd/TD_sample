using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class TargetingComponent : IInitializable, ITickable
    {
        private readonly SphereSensorData _sensorData;
        private Collider[] _buffer;
        private float _currenTime;
        
        private ReactiveProperty<Unit> _target;
        public Observable<Unit> Target => _target;
        
        public TargetingComponent(SphereSensorData sensorData)
        {
            _sensorData = sensorData;
        }

        void IInitializable.Initialize()
        {
            _buffer = new Collider[_sensorData.BufferSize];
            _target = new ReactiveProperty<Unit>();
        }

        
        private void OnLostTarget(Unit target)
        {
            _target.Value = null;
            target.OnDied -= OnLostTarget;
            target.OnReached -= OnLostTarget;
        }

        private bool IsTargetInRange()
        {
            return (_target.Value.Position - _sensorData.Center.position).magnitude <= _sensorData.Radius + 1f;
        }

        private bool TrySetupTarget(out Unit target)
        {
            var size = Physics.OverlapSphereNonAlloc(_sensorData.Center.position,
                _sensorData.Radius, _buffer, _sensorData.LayerMask);
            
            target = null;
            if(size == 0)
                return false;
            
            var selfPosition = _sensorData.Center.position;
            var minDistance = float.MaxValue;

            for (var i = 0; i < size; i++)
            {
                var collider = _buffer[i];
                if (!collider.TryGetComponent(out Unit obj))
                    continue;

                var targetPosition = collider.transform.position;

                var distanceVector = targetPosition - selfPosition;
                var targetDistance = distanceVector.sqrMagnitude;

                if (targetDistance < minDistance * minDistance)
                {
                    target = obj;
                    minDistance = targetDistance;
                }
            }
            
            return target != null;
        }

        void ITickable.Tick()
        {
            if (_target.Value)
            {
                if (IsTargetInRange())
                    return;
                
                OnLostTarget(_target.Value);
                return;
            }
            
            _currenTime += Time.deltaTime;
            if (_currenTime >= _sensorData.Interval)
            {
                if (TrySetupTarget(out var target))
                {
                    target.OnDied += OnLostTarget;
                    target.OnReached += OnLostTarget;
                    _target.Value = target;
                }
                _currenTime = 0f;
            }
        }
    }
}