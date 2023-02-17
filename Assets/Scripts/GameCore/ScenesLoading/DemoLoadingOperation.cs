using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace LiftGame.GameCore.ScenesLoading
{
    public class DemoLoadingOperation : ILoadingOperation
    {
        public async UniTask Load()
        {
            var loading = SceneManager.LoadSceneAsync(ScenesConstants.TEST_SCENE, 
                LoadSceneMode.Single);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
        public async UniTask Unload()
        {
            var loading = SceneManager.UnloadSceneAsync(ScenesConstants.TEST_SCENE);
            while (loading.isDone == false)
            {
                await UniTask.Delay(1);
            }
        }
    }
}