using LiftGame.FPSController;
using LiftGame.FPSController.CameraController;
using LiftGame.FPSController.FirstPersonController;
using LiftGame.Inventory;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerAirSystem;
using LiftGame.PlayerCore.PlayerCostume;
using LiftGame.PlayerCore.PlayerPowerSystem;
using LiftGame.PlayerEquipment;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore
{
    public class PlayerServiceProvider : MonoBehaviour
    {
        [SerializeField] private FirstPersonController fpsController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerCostumeService costumeService;
        [SerializeField] private PlayerLitStateProvider litStateProvider;
        [SerializeField] private EquipmentSwitcher equipmentSwitcher;
        private IPlayerHealthService _healthService;
        private IPlayerMentalService _mentalService;
        private IPlayerPowerService _powerService;
        private IPlayerAirService _airService;
        private IPlayerInventoryService _inventoryService;

        //MonoBehaviour injection
        [Inject]
        public void Construct(IPlayerHealthService playerHealthService, IPlayerMentalService playerMentalService,
            IPlayerPowerService playerPowerService, IPlayerAirService playerAirService,
            IPlayerInventoryService playerInventoryService)
        {
            _healthService = playerHealthService;
            _mentalService = playerMentalService;
            _powerService = playerPowerService;
            _airService = playerAirService;
            _inventoryService = playerInventoryService;
        }
        
        public FirstPersonController FPSController => fpsController;
        public CameraController CameraController => cameraController;
        public PlayerAnimationController AnimationController => animationController;
        public PlayerCostumeService CostumeService => costumeService;
        public PlayerLitStateProvider LitStateProvider => litStateProvider;
        public EquipmentSwitcher EquipmentSwitcher => equipmentSwitcher;
        public IPlayerHealthService HealthService => _healthService;
        public IPlayerMentalService MentalService => _mentalService;
        public IPlayerPowerService PowerService => _powerService;
        public IPlayerAirService AirService => _airService;
        public IPlayerInventoryService InventoryService => _inventoryService;
    }
}