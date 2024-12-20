using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public struct SphereSensorData
    {
        [field: SerializeField]
        public Transform Center { get;private set; }
        
        [field: SerializeField]
        public float Radius { get;private set; }
        
        [field: SerializeField]
        public float Interval { get;private set; }

        [field: SerializeField]
        public int BufferSize { get;private set; }

        [field: SerializeField]
        public LayerMask LayerMask { get;private set; }
    }
}