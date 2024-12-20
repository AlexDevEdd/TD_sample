using System;
using UnityEngine;

namespace GamePlay
{
    public class Cycle 
    {
        public enum State
        {
            IDLE = 0,
            PLAYING = 1,
            PAUSED = 2
        }

        public event Action OnStarted;
        public event Action OnStopped;
        public event Action OnPaused;
        public event Action OnResumed;

        public event Action OnCycle;
        public event Action<State> OnStateChanged;

        public event Action<float> OnCurrentTimeChanged;
        public event Action<float> OnProgressChanged;
        public event Action<float> OnDurationChanged;

        private float _duration;
        private float _currentTime;
        private State _currentState;

        public State CurrentState => _currentState;
        public float Duration => _duration;
        public float CurrentTime => _currentTime;
        public float Progress => GetProgress();
        
        public Cycle() { }

        public Cycle(float duration)
        {
            _duration = duration;
        }

        public State GetCurrentState() => _currentState;
        public bool IsPlaying() => _currentState == State.PLAYING;
        public bool IsPaused() => _currentState == State.PAUSED;
        public bool IsIdle() => _currentState == State.IDLE;

        public float GetDuration() => _duration;
        public float GetCurrentTime() => _currentTime;


        public bool Start()
        {
            if (_currentState is not State.IDLE)
                return false;

            _currentTime = 0;
            _currentState = State.PLAYING;
            OnStateChanged?.Invoke(State.PLAYING);
            OnStarted?.Invoke();
            return true;
        }

        public bool Start(float currentTime)
        {
            if (_currentState is not State.IDLE)
                return false;

            _currentTime = Mathf.Clamp(currentTime, 0, _duration);
            _currentState = State.PLAYING;
            OnStateChanged?.Invoke(State.PLAYING);
            OnStarted?.Invoke();
            return true;
        }

        public bool Pause()
        {
            if (_currentState != State.PLAYING)
                return false;

            _currentState = State.PAUSED;
            OnStateChanged?.Invoke(State.PAUSED);
            OnPaused?.Invoke();
            return true;
        }
        
        public bool Resume()
        {
            if (_currentState != State.PAUSED)
                return false;

            _currentState = State.PLAYING;
            OnStateChanged?.Invoke(State.PLAYING);
            OnResumed?.Invoke();
            return true;
        }
        
        public bool Stop()
        {
            if (_currentState == State.IDLE)
                return false;

            _currentTime = 0;
            _currentState = State.IDLE;
            OnStateChanged?.Invoke(State.IDLE);
            OnStopped?.Invoke();
            return true;
        }


        public void Tick(float deltaTime)
        {
            if (_currentState != State.PLAYING)
                return;

            _currentTime = Mathf.Min(_currentTime + deltaTime, _duration);
            OnCurrentTimeChanged?.Invoke(_currentTime);

            var progress = _currentTime / _duration;
            OnProgressChanged?.Invoke(progress);

            if (progress >= 1) 
                CompleteCycle();
        }

        private void CompleteCycle()
        {
            OnCycle?.Invoke();
            SetCurrentTime(_currentTime - _duration);
        }
        

        public void SetDuration(float duration)
        {
            if (duration < 0)
                return;

            if (Math.Abs(_duration - duration) > float.Epsilon)
            {
                _duration = duration;
                OnDurationChanged?.Invoke(duration);
            }
        }


        public void SetCurrentTime(float time)
        {
            if (time < 0)
                return;

            var newTime = Mathf.Clamp(time, 0, _duration);
            if (Math.Abs(newTime - _duration) > float.Epsilon)
            {
                _currentTime = newTime;
                OnCurrentTimeChanged?.Invoke(newTime);
            }
        }

        public float GetProgress()
        {
            return _currentState switch
            {
                State.PLAYING or State.PAUSED => _currentTime / _duration,
                _ => 0
            };
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            var newTime = _duration * progress;
            _currentTime = newTime;
            OnCurrentTimeChanged?.Invoke(newTime);
            OnProgressChanged?.Invoke(progress);
        }
    }
}