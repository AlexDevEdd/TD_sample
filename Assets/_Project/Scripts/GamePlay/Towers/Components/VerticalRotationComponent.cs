using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class VerticalRotationComponent : IInitializable
    {
        private readonly Transform _vertical;
        private readonly float _speed;
        
        private Quaternion _defaultRotation;
        
        public VerticalRotationComponent(VerticalRotationData data)
        {
            _vertical = data.Transform;;
            _speed = data.Speed;
        }
        
        public void Initialize()
        {
            _defaultRotation = _vertical.rotation;
        }
        
        public void Rotate(Vector3 targetPos, float deltaTime)
        {
            var targetRotation = Quaternion.LookRotation(targetPos);
            _vertical.rotation = Quaternion.RotateTowards(_vertical.rotation,
                targetRotation, _speed * deltaTime);
        }
        
        public void ResetRotation(float deltaTime)
        {
            _vertical.rotation = Quaternion.RotateTowards(_vertical.rotation,
                _defaultRotation, _speed * deltaTime);
        }
    }
}