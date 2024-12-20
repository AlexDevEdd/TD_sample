using System.Collections.Generic;
using GameCycle;
using JetBrains.Annotations;
using UnityEngine;

namespace GamePlay
{
    [UsedImplicitly]
    public class UnitWorld : IUnitSpawner, ITick
    {
        private readonly List<Unit> _units = new (); 
        private readonly IUnitPool<Unit> _unitPool;
        private readonly Transform _target;

        public UnitWorld(IUnitPool<Unit> unitPool, Transform target)
        {
            _unitPool = unitPool;
            _target = target;
        }
        
        public void Spawn(Vector3 position)
        {
            var unit = _unitPool.Get();
            _units.Add(unit);
            
            unit.SetPosition(position);
            unit.SetDestination(_target.position);
            
            unit.OnDied += Remove;
            unit.OnReached += Remove;
        }
        
        private void Remove(Unit unit)
        {
            unit.OnDied -= Remove;
            unit.OnReached -= Remove;
            _units.Remove(unit);
            _unitPool.Put(unit);
        }

        public void Tick(float delta)
        {
            for (var i = 0; i < _units.Count; i++) 
                _units[i]?.Tick(delta);
        }
    }
}