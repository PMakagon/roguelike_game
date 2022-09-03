using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LiftGame.GameCore.ScenesLoading
{
    class HubLoadingOperation : ILoadingOperation
    {
        public async UniTask Load()
        {
            var loading = SceneManager.LoadSceneAsync(ScenesConstants.HUB_SCENE, 
                LoadSceneMode.Additive);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        } 
        public async UniTask Unload()
        {
            var loading = SceneManager.UnloadSceneAsync(ScenesConstants.HUB_SCENE);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
    }
}