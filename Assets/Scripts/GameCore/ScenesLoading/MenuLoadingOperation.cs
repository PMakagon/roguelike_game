using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LiftGame.GameCore.ScenesLoading
{
    class MenuLoadingOperation : ILoadingOperation
    {
        public async UniTask Load()
        {
            var loading = SceneManager.LoadSceneAsync(ScenesConstants.MAIN_MENU, 
                LoadSceneMode.Single);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
        public async UniTask Unload()
        {
            var loading = SceneManager.UnloadSceneAsync(ScenesConstants.MAIN_MENU);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
      
    }
}