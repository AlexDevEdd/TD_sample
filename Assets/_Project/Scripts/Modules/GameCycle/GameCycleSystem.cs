using System.Linq;
using JetBrains.Annotations;

namespace GameCycle
{
    [UsedImplicitly]
    public class GameCycleSystem
    {
        private readonly IGameStart[] _startListeners;
        private readonly IGamePause[] _pauseListener;
        private readonly IGameResume[] _resumeListeners;
        private readonly IGameFinish[] _finishListeners;
        
        public GameState GameState { get; private set; }
        
        public GameCycleSystem(IGameListener[] listeners) 
        {
            _startListeners = listeners.OfType<IGameStart>().ToArray();
            _pauseListener = listeners.OfType<IGamePause>().ToArray();
            _resumeListeners = listeners.OfType<IGameResume>().ToArray();
            _finishListeners = listeners.OfType<IGameFinish>().ToArray();
        }
        
        
        public void OnStartEvent()
        {
            if (GameState != GameState.None)
                return;

            ChangeState(GameState.Start);

            for (int i = 0; i < _startListeners.Length; i++)
                _startListeners[i].OnStart();

            ChangeState(GameState.Running);
        }

        public void OnPauseEvent()
        {
            if (GameState != GameState.Running)
                return;

            ChangeState(GameState.Pause);

            for (int i = 0; i < _pauseListener.Length; i++)
                _pauseListener[i].OnPause();
        }

        public void OnResumeEvent()
        {
            if (GameState != GameState.Pause)
                return;

            ChangeState(GameState.Running);

            for (int i = 0; i < _resumeListeners.Length; i++)
                _resumeListeners[i].OnResume();
        }
        
        public void OnFinish()
        {
            if (GameState is GameState.Finish or GameState.None or GameState.Start)
                return;

            ChangeState(GameState.Finish);
            foreach (var listener in _finishListeners)
                listener.OnFinish();
        }

        public bool IsPaused()
        {
            return GameState != GameState.Running;
        }

        private void ChangeState(GameState state)
        {
            if (Equals(GameState, state)) return;
            GameState = state;
        }
        
        public void Dispose()
        {
            OnFinish();
        }
    }
}