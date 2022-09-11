using LiftGame.GameCore.ScenesLoading;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore
{
    public class AppStart : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        private void Start()
        {
            _sceneLoaderService.LoadMainMenu();
        }
    }
}