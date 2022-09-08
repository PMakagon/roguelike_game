using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;

namespace LiftGame.GameCore.Input
{
    public interface IPlayerInputService : IPauseable
    {
        void Initialize(InputData inputData);
        void UpdateInput();
        void SetInputActive(bool state);
        void ResetInput();
    }
}