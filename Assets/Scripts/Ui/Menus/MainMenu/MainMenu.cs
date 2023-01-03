using Cysharp.Threading.Tasks;
using LiftGame.GameCore.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiftGame.Ui.Menus.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button newGameBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private LoadingIcon loadingIcon;
        [SerializeField] private MainMenuAnimation _animation;

        private ISceneLoaderService _sceneLoaderService;
        
        [Inject]
        public void Construct(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }
        private void Start()
        {
            newGameBtn.onClick.AddListener(OnNewGameBtnClicked);
            exitBtn.onClick.AddListener(OnExitBtnClicked);
            _animation.PlayOpenAnimation();
        }

        private void HideMenuButtons()
        {
            newGameBtn.gameObject.SetActive(false);
            exitBtn.gameObject.SetActive(false);
        }

        private void Dummy()
        {
            _sceneLoaderService.UnloadMainMenu(OnMenuUnload);
        }

        private void OnMenuUnload()
        {
            loadingIcon.Disable();
        }

        private async void OnNewGameBtnClicked()
        {
            HideMenuButtons();
            loadingIcon.gameObject.SetActive(true);
            loadingIcon.Enable();
            _animation.PlayCloseAnimation();
            await UniTask.Delay(4000);
            await _sceneLoaderService.LoadNewGame(Dummy);
        }

        private void OnExitBtnClicked()
        {
           Application.Quit();
        }
    }
}
