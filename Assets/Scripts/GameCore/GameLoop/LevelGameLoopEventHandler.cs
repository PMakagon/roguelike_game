using System;
using UnityEngine;

namespace LiftGame.GameCore.GameLoop
{
    public class LevelGameLoopEventHandler : ILevelGameLoopEventHandler
    {
        public static event Action OnLoopStart = delegate {  };
        public static event Action OnLoopEnd = delegate {  };
        public static event Action OnGameOver = delegate {  };
        public bool IsPlayerOnLevel { get; set; }

        public void StartLoop()
        {
            IsPlayerOnLevel = true;
            OnLoopStart?.Invoke();
        }

        public void EndLoop()
        {
            IsPlayerOnLevel = false;
            OnLoopEnd?.Invoke();
        }

        public void GameOver()
        {
            OnGameOver?.Invoke();
            
        }
    }
}