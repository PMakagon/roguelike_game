using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LiftGame.GameCore.ScenesLoading
{
    class NewGameLoadingOperation : ILoadingOperation
    {
        public async UniTask Load()
        {
            var loading = SceneManager.LoadSceneAsync(ScenesConstants.NEW_GAME, 
                LoadSceneMode.Additive);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
        public async UniTask Unload()
        {
            var loading = SceneManager.UnloadSceneAsync(ScenesConstants.NEW_GAME);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
    }
}