using UnityEngine;

namespace GamePlay
{
    public class TowerGizmos : MonoBehaviour
    {
        [SerializeField]
        private SphereSensorData _sensorData;
        
        private void OnDrawGizmosSelected()
        {
            DrawSensor();
        }
        
        private void DrawSensor()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_sensorData.Center.position, _sensorData.Radius);
        }
    }
}