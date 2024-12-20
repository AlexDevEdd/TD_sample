using UnityEngine;

namespace GamePlay
{
    public interface IProjectileFactory
    {
        Projectile Create(ProjectileType type, FireMode fireMode, Transform firePoint, 
            Transform target, Vector3 predictionPoint);
    }
}