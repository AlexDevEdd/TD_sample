using System;
using UnityEngine;

namespace GamePlay
{
   
    [Serializable]
    public struct VerticalRotationData
    {
        [field: SerializeField]
        public Transform Transform { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}