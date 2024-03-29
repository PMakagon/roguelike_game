﻿using System;
using LiftGame.GameCore.Pause;

namespace LiftGame.PlayerCore.MentalSystem
{
    public interface IPlayerMentalService : IPauseable
    {
        bool IsStressChangeEnabled { get; }
        float GetStressLevel();
        StressState GetCurrentState();
        void AddStress(int stressAmount);
        void ReduceStress(int stressAmount);
        void EnableStressChange();
        void DisableStressChange();
        void SetStartState();
        void SetSafeState();
    }
}