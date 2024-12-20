using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public sealed class MoveComponent
    {
        public event Action OnDestinationReached;
        
        [SerializeField]
        private Rigidbody _rigidbody;
        
        [SerializeField]
        private float _moveSpeed;
        
        [SerializeField]
        private float _stopDistance;

        private Vector3 _destination;
        private bool _isReached;

        public void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _isReached = false;
        }
        
        public void Move(float deltaTime)
        {
            if (_isReached) return;
        
            if (Vector3.Distance(_rigidbody.position, _destination) <= _stopDistance)
            {
                _rigidbody.velocity = Vector3.zero;
                OnDestinationReached?.Invoke();
                _isReached = true;
                return;
            }
            
            var delta = _moveSpeed * deltaTime;
            var newPosition = Vector3.MoveTowards(_rigidbody.position, _destination, delta);
            var direction = (newPosition - _rigidbody.position).normalized;
            
            _rigidbody.velocity = direction * _moveSpeed;
            
        }
    }
}