using System;
using UnityEngine;

namespace GamePlay
{
    public sealed class ProjectileBuilder 
    {
        private Projectile _projectile;

        public ProjectileBuilder() { } 
        public ProjectileBuilder(Projectile projectile)
        {
            _projectile = projectile;
        }

        public ProjectileBuilder SetProjectile(Projectile projectile)
        {
            _projectile = projectile;
            return this;
        }

        public ProjectileBuilder SetPosition(Vector3 position, Quaternion rotation)
        {
            EnsureProjectileExists();
            _projectile.SetPosition(position, rotation);
            return this;
        }

        public ProjectileBuilder SetTarget(Transform target)
        {
            EnsureProjectileExists();
            _projectile.SetTarget(target);
            return this;
        }

        public ProjectileBuilder SetVelocity(Vector3 velocity)
        {
            EnsureProjectileExists();
            _projectile.SetVelocity(velocity);
            return this;
        }
        
        public ProjectileBuilder SetMoveStrategy(MoveStrategy moveStrategy)
        {
            EnsureProjectileExists();
            _projectile.SetMoveStrategy(moveStrategy);
            return this;
        }
        
        public ProjectileBuilder IsUpdatable(bool isUpdatable)
        {
            EnsureProjectileExists();
            _projectile.SetIsUpdatable(isUpdatable);
            return this;
        }
        
        public ProjectileBuilder IsUseGravity(bool isUseGravity)
        {
            EnsureProjectileExists();
            _projectile.IsUseGravity(isUseGravity);
            return this;
        }
        
        public Projectile Build()
        {
            EnsureProjectileExists();
            return _projectile;
        }
        
        public Projectile BuildWithInit()
        {
            EnsureProjectileExists();
            _projectile.Init();
            return _projectile;
        }

        public ProjectileBuilder Clear()
        {
            _projectile = null;
            return this;
        }

        private void EnsureProjectileExists()
        {
            if (_projectile == null)
            {
                throw new NullReferenceException("Projectile doesn't exist in projectile builder. Use SetProjectile method");
            }
        }
    }
}