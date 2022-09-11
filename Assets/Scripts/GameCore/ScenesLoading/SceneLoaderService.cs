using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LiftGame.GameCore.ScenesLoading
{
    public class SceneLoaderService :  ISceneLoaderService
    {
        
        public async UniTask LoadMainMenu()
        {
            var operation = new MenuLoadingOperation();
            await operation.Load();
        }

        public async UniTask UnloadMainMenu(Action onMenuUnload)
        {
            var operation = new MenuLoadingOperation();
            await operation.Unload();
        }
        
        public async UniTask UnloadGame()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new NewGameLoadingOperation());
            operations.Enqueue(new HubLoadingOperation());
            foreach (var operation in operations)
            {
                await operation.Unload();
            }
        }

        public async UniTask LoadNewGame(Action onNewGameLoad)
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new NewGameLoadingOperation());
            operations.Enqueue(new HubLoadingOperation());
            foreach (var operation in operations)
            {
               await operation.Load();
            }
            onNewGameLoad?.Invoke();
        }
        
        
        public async UniTask LoadHub(Action onHubLoad)
        {
            var operation = new HubLoadingOperation();
            await operation.Load();
            onHubLoad?.Invoke();
        }
       
        public async UniTask UnloadHub(Action onHubUnload)
        {
            var unloading = SceneManager.UnloadSceneAsync(ScenesConstants.HUB_SCENE);
            while (unloading.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onHubUnload?.Invoke();
        }
        
    }
    
}