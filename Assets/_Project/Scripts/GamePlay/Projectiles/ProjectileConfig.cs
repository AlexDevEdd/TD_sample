using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(
        fileName = "ProjectileConfig", 
        menuName = "Projectiles/New ProjectileConfig")]
    public sealed class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField]
        public ProjectileType Type { get; private set; }
        
        [field:SerializeField] 
        public int Damage { get; private set; }
        
        [field:SerializeField] 
        public float ProjectileSpeed { get; private set; }
        
        [field:SerializeField] 
        public float LifeTime { get; private set; }
    }
}