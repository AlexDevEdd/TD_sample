namespace GamePlay
{
    public interface IProjectilePool
    {
        ProjectileType Type { get; }
        ProjectileBuilder Get();
        void Put(Projectile projectile);
    }
}