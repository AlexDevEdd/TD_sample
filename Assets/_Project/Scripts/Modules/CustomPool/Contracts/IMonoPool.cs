using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CustomPool
{
    public interface IMonoPool<TContract> where TContract : MonoBehaviour
    {
        public event Action<TContract> OnCreatedAction;
        public event Action<TContract> OnSpawnAction;
        public event Action<TContract> OnDeSpawnAction;
        
        public HashSet<TContract> Actives { get; }
        public UniTaskVoid Init();
        public TContract Spawn();
        public void DeSpawn(TContract item);
    }
}