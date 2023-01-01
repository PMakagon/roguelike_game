﻿using System;
using LiftGame.GameCore;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.GameCore.ScenesLoading;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.Ui.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace LiftGame.Ui
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject screen;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private LoadingIcon loadingIcon;
        
        private ISceneLoaderService _sceneLoader;

        // MonoBehaviour injection
        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoader = sceneLoaderService;
        }
        private void Start()
        {
            restartBtn.onClick.AddListener(RestartGame);
            menuBtn.onClick.AddListener(LoadMainMenu);
            exitBtn.onClick.AddListener(OnExitBtnClicked);
            LevelGameLoopEventHandler.OnGameOver += OpenGameOverScreen;
        }

        private void OnDestroy()
        {
            LevelGameLoopEventHandler.OnGameOver -= OpenGameOverScreen;
        }

        private void OpenGameOverScreen()
        {
            screen.SetActive(true);
            Debug.Log("HELLO");
        }

        private void HideMenuButtons()
        {
            restartBtn.gameObject.SetActive(false);
            menuBtn.gameObject.SetActive(false);
            exitBtn.gameObject.SetActive(false);
        }

        private void DeleteTempScene()
        {
            SceneManager.UnloadSceneAsync("GameOverTempScene");
        }
        
        private void RestartGame()
        {
            loadingIcon.gameObject.SetActive(true);
            loadingIcon.Enable();
            HideMenuButtons();
            SceneManager.MoveGameObjectToScene(this.gameObject,SceneManager.CreateScene("GameOverTempScene"));
            _sceneLoader.UnloadGame();
            _sceneLoader.LoadNewGame(DeleteTempScene);
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