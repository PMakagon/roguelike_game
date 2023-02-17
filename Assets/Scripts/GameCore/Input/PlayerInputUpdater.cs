using LiftGame.GameCore.Input.Data;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore.Input
{
    public class PlayerInputUpdater : MonoBehaviour
    {
        private InputDataProvider _inputDataProvider;
        private IPlayerInputService _inputService;

        //MonoBehaviour injection
        [Inject]
        private void Construct(IPlayerInputService playerInputService,InputDataProvider inputDataProvider)
        {
            _inputService = playerInputService;
            _inputDataProvider = inputDataProvider;
        }

        private void Start()
        {
            _inputService.Initialize(_inputDataProvider);
        }

        private void Update()
        {
            _inputService.UpdateInput();
        }
    }
}