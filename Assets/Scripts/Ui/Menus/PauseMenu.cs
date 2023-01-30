using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.GameCore.Pause;
using LiftGame.GameCore.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiftGame.Ui.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject screen;
        [SerializeField] private GameObject confirmPanel;
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private LoadingIcon loadingIcon;

        private IPauseHandler _pauseHandler;
        private ISceneLoaderService _sceneLoader;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPauseHandler pauseHandler)
        {
            _sceneLoader = sceneLoaderService;
            _pauseHandler = pauseHandler;
        }

        private void Start()                        
        {
            continueBtn.onClick.AddListener(ContinueGame);
            menuBtn.onClick.AddListener(LoadMainMenu);
            exitBtn.onClick.AddListener(OnExitBtnClicked);
            screen.SetActive(false);
            NonGameplayInputData.OnPauseMenuClicked += OnPausePressed;
            LevelGameLoopEventHandler.OnGameOver += Unsubscribe;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            NonGameplayInputData.OnPauseMenuClicked -= OnPausePressed;
            LevelGameLoopEventHandler.OnGameOver -= Unsubscribe;
        }

        private void OnPausePressed()
        {
            var isPaused = _pauseHandler.IsPaused;
            SetPauseAndScreenState(!isPaused);
        }

        private void SetPauseAndScreenState(bool state)
        {
            screen.SetActive(state);
            _pauseHandler.SetPaused(state);
            Cursor.visible = state;
            Cursor.lockState = state? CursorLockMode.Confined : CursorLockMode.Locked ;
        }
        
        private void ContinueGame()
        {
            SetPauseAndScreenState(false);
        }

        private void HideMenuButtons()
        {
            continueBtn.gameObject.SetActive(false);
            menuBtn.gameObject.SetActive(false);
            exitBtn.gameObject.SetActive(false);
        }

        private void LoadMainMenu()
        {
            HideMenuButtons();
            _sceneLoader.LoadMainMenu();
        }
        
        private void OnExitBtnClicked()
        {
            Application.Quit();
        }
    }
}