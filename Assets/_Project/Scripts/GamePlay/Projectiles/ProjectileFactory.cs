using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class ProjectileFactory : IProjectileFactory
    {
        private readonly Dictionary<ProjectileType, IProjectilePool> _pools;
        
        public ProjectileFactory(IEnumerable<IProjectilePool> pools)
        {
            _pools = new Dictionary<ProjectileType, IProjectilePool>();
            
            foreach (var pool in pools) 
                _pools.TryAdd(pool.Type, pool); 
        }
        
        public Projectile Create(ProjectileType type, FireMode fireMode, Transform firePoint, 
            Transform target, Vector3 predictionPoint)
        {
            if (!_pools.TryGetValue(type, out var pool))
                throw new KeyNotFoundException($"key {type} for projectile pool not found");
            
            var projectile = pool.Get()
                .SetPosition(firePoint.position, firePoint.rotation)
                .SetTarget(target)
                .SetVelocity(predictionPoint)
                .IsUseGravity(fireMode is FireMode.Parabolic)
                .BuildWithInit();
            
            return projectile;
        }
    }
}