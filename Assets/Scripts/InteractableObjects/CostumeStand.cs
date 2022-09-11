using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.PlayerCostume;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.InteractableObjects
{
    public class CostumeStand : Interactable
    {
        [SerializeField] private Costume costume;
        private IPlayerCostumeService _playerCostumeService;
        private IPlayerPowerService _playerPowerService;


        [Inject]
        private void Construct(PlayerServiceProvider playerServiceProvider,IPlayerPowerService playerPowerService)
        {
            _playerPowerService = playerPowerService;
            _playerCostumeService = playerServiceProvider.PlayerCostumeService;
        }
        
        public override void OnInteract(IPlayerData playerData)
        {
            _playerCostumeService.SetCostumeActive(true);
            _playerPowerService.SetActive(true);
            Destroy(gameObject);
        }
    }
}