using JetBrains.Annotations;
using UnityEngine;

namespace GamePlay
{
   
    [UsedImplicitly]
    public sealed class AttackComponent
    {
        private readonly IProjectileFactory _projectileFactory;
        private readonly ProjectileType _projectileType;
        
        public float ProjectileSpeed { get; }
        public float AttackSpeed { get; }
        public Transform FirePoint { get; }
        public FireMode FireMode { get; }

        public AttackComponent(IProjectileFactory projectileFactory, AttackData data)
        {
            _projectileFactory = projectileFactory;
            
            _projectileType = data.Config.Type;
            ProjectileSpeed = data.Config.ProjectileSpeed;
            AttackSpeed = data.AttackSpeed;
            FirePoint = data.FirePoint;
            FireMode = data.FireMode;
        }

        public void Fire(Transform target, Vector3 predictTrajectory)
        {
            var projectile =_projectileFactory.Create(_projectileType, FireMode, FirePoint, target, predictTrajectory);
            projectile.Move();
        }
    }
}