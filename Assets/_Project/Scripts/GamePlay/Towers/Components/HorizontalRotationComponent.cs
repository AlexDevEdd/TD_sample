using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class HorizontalRotationComponent : IInitializable
    {
        private readonly Transform _horizontal;
        private readonly float _speed;
        private readonly float _thresholdSensitivity;
        private readonly float _minThresholdAngle;
        private readonly float _maxThresholdAngle;
        
        private Quaternion _defaultRotation;
        private Vector3 _predictedPos;
        
        public HorizontalRotationComponent(HorizontalRotationData data)
        {
            _thresholdSensitivity = data.ThresholdSensitivity;
            _minThresholdAngle = data.MinThresholdAngle;
            _maxThresholdAngle = data.MaxThresholdAngle;
            _horizontal = data.Transform;
            _speed = data.Speed;
        }
        
        public void Initialize()
        {
            _defaultRotation = _horizontal.rotation;
        }

        public void Rotate(Vector3 predictedPos, float deltaTime)
        {
            predictedPos.y = 0;
            var targetRotation = Quaternion.LookRotation(predictedPos);
            _horizontal.rotation = Quaternion.RotateTowards(_horizontal.rotation,
                targetRotation, _speed * deltaTime);
            
            _predictedPos = predictedPos;
        }

        public void ResetRotation(float deltaTime)
        {
            _horizontal.rotation = Quaternion.RotateTowards(_horizontal.rotation,
                _defaultRotation, _speed * deltaTime);
        }
        
        public bool IsLookOnTarget()
        {
            var directionToTarget = (_predictedPos - _horizontal.position).normalized;
            var dotProduct = Vector3.Dot(_horizontal.forward, directionToTarget);
    
            var angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
            var distanceToTarget = (_predictedPos - _horizontal.position).magnitude;
            
            var angleThreshold = Mathf.Clamp(_thresholdSensitivity * distanceToTarget,
                _minThresholdAngle, _maxThresholdAngle);
            
            return angle < angleThreshold;
        }
    }
}