using System;
using UnityEngine;

namespace GamePlay
{
    public class Unit : MonoBehaviour, IDamageable
    {
        public event Action<Unit> OnDied;
        public event Action<Unit> OnReached;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [SerializeField]
        private Transform _aim;
        
        private Rigidbody _rigidbody;
        public Transform Aim => _aim;
        public Vector3 Position => transform.position;
        public Vector3 Velocity => _rigidbody.velocity;
        public bool IsAlive => _healthComponent.IsAlive;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _healthComponent.OnDied += OnDiedAction;
            _moveComponent.OnDestinationReached += OnDestinationReached;
            _healthComponent.Reset();
        }

        private void OnDisable()
        {
            _healthComponent.OnDied -= OnDiedAction;
            _moveComponent.OnDestinationReached -= OnDestinationReached;
        }

        public void Tick(float delta)
        {
            _moveComponent.Move(delta);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetDestination(Vector3 destination)
        {
            _moveComponent.SetDestination(destination);
        }

        public void ApplyDamage(float damage)
        {
            _healthComponent.ApplyDamage(damage);
        }

        private void OnDiedAction()
        {
            OnDisable();
            OnDied?.Invoke(this);
        }

        private void OnDestinationReached()
        {
            OnDisable();
            OnReached?.Invoke(this);
        }
    }
}