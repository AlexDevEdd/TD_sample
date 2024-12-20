using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public sealed class HealthComponent
    {
        public event Action OnDied;
        
        [SerializeField]
        private float _maxHealth = 30;
        private float _health;
        public bool IsAlive => _health > 0;

        public void Reset()
        {
            _health = _maxHealth;
        }

        [Button]
        public void ApplyDamage(float damage)
        {
            _health = Mathf.Max(0, _health - damage);
            
            if (_health <= 0) 
                OnDied?.Invoke();
        }
    }
}