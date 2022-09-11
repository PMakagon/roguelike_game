using System;

namespace LiftGame.GameCore.GameLoop
{
    public interface ILevelGameLoopEventHandler
    { 
        static event Action OnLoopStart;
        static event Action OnLoopEnd;
        static event Action OnGameOver;
        bool IsPlayerOnLevel { get; set; }
        void StartLoop();
        void EndLoop();
        void GameOver();
    }
}