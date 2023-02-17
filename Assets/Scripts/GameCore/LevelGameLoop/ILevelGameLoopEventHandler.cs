using System;

namespace LiftGame.GameCore.LevelGameLoop
{
    public interface ILevelGameLoopEventHandler
    { 
        static event Action OnLoopStart;
        static event Action OnLoopEnd;
        static event Action OnGameOver;
        bool IsPlayerOnLevel { get; set; }
        void TryStartLoop();
        void TryEndLoop();
        void GameOver();
    }
}