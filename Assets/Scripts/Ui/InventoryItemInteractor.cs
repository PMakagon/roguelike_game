using LiftGame.Inventory;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerPowerSystem;
using Zenject;

namespace LiftGame.Ui
{
    public class InventoryItemInteractor
    {
        private IPlayerHealthService _healthService;
        private IPlayerMentalService _mentalService;
        private IPlayerPowerService _powerService;
        private IPlayerInventoryService _inventoryService;
        private PlayerServiceProvider _serviceProvider;

        public IPlayerHealthService HealthService => _healthService;

        public IPlayerMentalService MentalService => _mentalService;

        public IPlayerPowerService PowerService => _powerService;

        public IPlayerInventoryService InventoryService => _inventoryService;

        public PlayerServiceProvider ServiceProvider => _serviceProvider;


        [Inject]
        public InventoryItemInteractor(PlayerServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _healthService = serviceProvider.HealthService;
            _mentalService = serviceProvider.MentalService;
            _powerService = serviceProvider.PowerService;
            _inventoryService = serviceProvider.InventoryService;
        }

        public void UseItem(IInventoryItem item)
        {
            if (item is not ConsumableItem useable) return;
            useable.Use(this);
        }

        public bool EquipItem()
        {
            return false;
        }

        public bool UnequipItem()
        {
            return false;
        }

        public void DropItem(IInventoryItem item)
        {
            if (item.canDrop)
            {
                (item as ItemDefinition)?.SpawnWorldItem(_serviceProvider.FPSController.transform.position);
            }
        }
    }
}