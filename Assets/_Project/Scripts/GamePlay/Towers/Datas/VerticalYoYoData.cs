using System;
using UnityEngine;

namespace GamePlay
{
    [Serializable]
    public struct VerticalYoYoData
    {
        [field: SerializeField]
        public Transform Transform { get; private set; }
        
        [field: SerializeField]
        public float Max { get; private set; }
        
        [field: SerializeField]
        public float Duration { get; private set; }
    }
}