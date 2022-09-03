using System;
using Cysharp.Threading.Tasks;

namespace LiftGame.GameCore.ScenesLoading
{
    public interface ISceneLoaderService
    {
        UniTask LoadMainMenu();
        UniTask UnloadMainMenu(Action onMenuUnload);
        UniTask LoadNewGame(Action onNewGameLoad);
        UniTask UnloadGame();
        UniTask LoadHub(Action onHubLoad);
        UniTask UnloadHub(Action onHubUnload);
    }
}