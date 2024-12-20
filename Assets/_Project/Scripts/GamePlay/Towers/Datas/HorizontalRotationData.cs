using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public struct HorizontalRotationData
    {
        [field: SerializeField]
        public Transform Transform { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }
        
        [field: SerializeField]
        public float ThresholdSensitivity { get; private set; }
        
        [field: SerializeField]
        public float MinThresholdAngle { get; private set; }
        
        [field: SerializeField]
        public float MaxThresholdAngle { get; private set; }
    }
}