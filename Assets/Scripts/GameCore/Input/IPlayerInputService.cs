using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;

namespace LiftGame.GameCore.Input
{
    public interface IPlayerInputService : IPauseable
    {
        void Initialize(InputDataProvider inputDataProvider);
        void UpdateInput();
        void SetInputActive(bool state);
        void ResetInput();
    }
}