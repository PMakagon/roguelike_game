using System;
using LiftGame.GameCore.Input.Data;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore.Input
{
    public class PlayerInputUpdater : MonoBehaviour
    {
        [SerializeField] private InputDataProvider inputDataProvider;
        private IPlayerInputService _inputService;

        [Inject]
        private void Construct(IPlayerInputService playerInputService)
        {
            _inputService = playerInputService;
        }

        private void Start()
        {
            _inputService.Initialize(inputDataProvider);
        }

        private void Update()
        {
            _inputService.UpdateInput();
        }
    }
}