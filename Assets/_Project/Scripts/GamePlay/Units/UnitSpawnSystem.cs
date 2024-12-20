using System;
using GameCycle;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class UnitSpawnSystem : IGameStart, IDisposable
    {
        private readonly IUnitSpawner _spawner;
        private readonly UnitSpawnPointsHolder _pointsHolder;
        private readonly float _spawnInterval;
        private IDisposable _disposable;
        
        public UnitSpawnSystem(IUnitSpawner spawner, UnitSpawnPointsHolder pointsHolder, float spawnInterval)
        {
            _spawner = spawner;
            _pointsHolder = pointsHolder;
            _spawnInterval = spawnInterval;
        }
        
        public void OnStart()
        {
            SpawnUnit();
            
            _disposable = Observable
                .Interval(TimeSpan.FromSeconds(_spawnInterval))
                .Subscribe(OnSpawnUnit);
        }
        
        private void OnSpawnUnit(R3.Unit _)
        {
            SpawnUnit();
        }

        private void SpawnUnit()
        {
            _spawner.Spawn(_pointsHolder.GetRandomPosition());
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}