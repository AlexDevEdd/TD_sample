using System;
using PrimeTween;
using UnityEngine;

namespace GamePlay
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> OnDead;
        
        [SerializeField]
        private ProjectileConfig _config;

        private MoveStrategy _moveStrategy;
        private Rigidbody _rigidbody;
       
        private bool _isUpdatable;
        private MoveParams _moveParams;
        
        private Tween _lifeTimeTween;
        
        public ProjectileType Type => _config.Type;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _moveParams = new MoveParams(_rigidbody, _config.ProjectileSpeed);
        }

        public void Init()
        {
            _lifeTimeTween = Tween.Delay(_config.LifeTime)
                .OnComplete(target:this, target => OnDead?.Invoke(this));
        }
        
        public void SetTarget(Transform target)
        {
            _moveParams.SetTarget(target);
        }
        
        public void SetVelocity(Vector3 velocity)
        {
            _moveParams.SetVelocity(velocity);
        }
        
        public void SetPosition(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }
        
        public void SetIsUpdatable(bool isUpdatable)
        {
            _isUpdatable = isUpdatable;
        }
        
        public void SetMoveStrategy(MoveStrategy moveStrategy)
        {
            _moveStrategy = moveStrategy;
        }

        public void IsUseGravity(bool isUseGravity)
        {
            _rigidbody.useGravity = isUseGravity;
        }

        public void Move()
        {
            _moveStrategy?.Move(_moveParams);
        }

        private void Update()
        {
            if(_isUpdatable)
                Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ApplyDamage(_config.Damage);
                OnDead?.Invoke(this);
            }
        }

        private void OnDisable()
        {
            _lifeTimeTween.Stop();
        }

        private void OnDestroy()
        {
            _lifeTimeTween.Stop();
        }
    }
}

