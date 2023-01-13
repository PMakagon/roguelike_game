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
        private Interaction _toEquipCostume = new Interaction("Equip",2, true);

        [Inject]
        private void Construct(PlayerServiceProvider playerServiceProvider,IPlayerPowerService playerPowerService)
        {
            _playerPowerService = playerPowerService;
            _playerCostumeService = playerServiceProvider.PlayerCostumeService;
        }
        
        public override void BindInteractions()
        {
            _toEquipCostume.actionOnInteract = EquipCostume;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toEquipCostume);
        }

        private bool EquipCostume()
        {
            _playerCostumeService.SetCostumeActive(true);
            _playerPowerService.SetActive(true);
            Destroy(gameObject);
            return true;
        }
        
    }
}