using System;

namespace LiftGame.PlayerCore.MentalSystem
{
    public interface IPlayerMentalService
    {
        event Action<int> OnStressAdd;
        void AddStress(int stressAmount);
        void ReduceStress(int stressAmount);
        void EnableStressChange();
        void DisableStressChange();
        void SetStartState();
        void SetSafeState();
    }
}