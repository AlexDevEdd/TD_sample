using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCycle
{
    public class GameKernel : MonoKernel
    {
        [Inject] private GameCycleSystem _gameCycleSystem;
        
        [Inject(Optional = true, Source = InjectSources.Local)]
        private readonly List<ITick> _ticks = new ();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private readonly List<IFixedTick> _fixedTicks = new ();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private readonly List<ILateTick> _lateTicks = new ();

        public override void Update()
        {
            base.Update();

            if (_gameCycleSystem.IsPaused())
                return;
            
            var deltaTime = Time.deltaTime;
            
            foreach (var tick in _ticks) 
                tick.Tick(deltaTime);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_gameCycleSystem.IsPaused())
                return;
            
            var fixedTime = Time.fixedTime;
            
            foreach (var tick in _fixedTicks)
                tick.FixedTick(fixedTime);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            
            if (_gameCycleSystem.IsPaused())
                return;
            
            var deltaTime = Time.deltaTime;

            foreach (var tick in _lateTicks)
                tick.LateTick(deltaTime);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _gameCycleSystem.OnFinish();
        }
    }
}