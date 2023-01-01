using System;

namespace LiftGame.GameCore.LevelGameLoop
{
    public class LevelGameLoopEventHandler : ILevelGameLoopEventHandler
    {
        public static event Action OnLoopStart = delegate { };
        public static event Action OnLoopEnd = delegate { };
        public static event Action OnGameOver = delegate { };
        public bool IsPlayerOnLevel { get; set; }

        public void TryStartLoop()
        {
            if (IsPlayerOnLevel) return;
            IsPlayerOnLevel = true;
            OnLoopStart?.Invoke();
        }

        public void TryEndLoop()
        {
            if (!IsPlayerOnLevel) return;
            IsPlayerOnLevel = false;
            OnLoopEnd?.Invoke();
        }

        public void GameOver()
        {
            IsPlayerOnLevel = false;
            OnGameOver?.Invoke();
        }
    }
}