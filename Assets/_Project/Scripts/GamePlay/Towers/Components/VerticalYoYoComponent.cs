using System;
using JetBrains.Annotations;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class VerticalYoYoComponent : IInitializable, IDisposable
    {
        private readonly Transform _vertical;
        private readonly float _max;
        private readonly float _duration;
        
        private Tween _yoyoTween;
        
        public VerticalYoYoComponent(VerticalYoYoData data)
        {
            _vertical = data.Transform;;
            _duration = data.Duration;  
            _max = data.Max;
        }
        
        void IInitializable.Initialize()
        {
            _yoyoTween = Tween.PositionY(_vertical, _max, _duration, Ease.Linear,
                cycles: -1, cycleMode: CycleMode.Yoyo);
        }
        
        void IDisposable.Dispose()
        {
            _yoyoTween.Stop();
        }
    }
}