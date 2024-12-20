using UnityEngine;

namespace GamePlay
{
    public sealed class UnitSpawnPointsHolder : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPoints;
        
        public Vector3 GetRandomPosition()
        {
            return _spawnPoints.Length <= 1 
                ? transform.position 
                : _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        }
    }
}