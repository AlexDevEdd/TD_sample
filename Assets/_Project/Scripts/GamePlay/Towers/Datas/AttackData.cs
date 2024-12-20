using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    [Serializable]
    public struct AttackData
    {
        [field: SerializeField]
        public FireMode FireMode { get; private set; }

        [field: SerializeField]
        public Transform FirePoint { get; private set; }

        [field: SerializeField]
        public float AttackSpeed { get; private set; }

        [field: SerializeField]
        public ProjectileConfig Config { get; private set; }
    }
}