using FarrokhGames.Inventory;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.NewInventory.Container
{
    [RequireComponent(typeof(InventoryRenderer))]
    public class ContainerPanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer renderer;
        private ContainerItemProvider _provider;
        private ContainerInventoryManager _inventoryManager;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        
        private void Start()
        {
            _inventoryService.onInventoryLoad += Init;
            
        }

        private void Init()
        {
            _inventoryManager = new ContainerInventoryManager();
        }
        
        private void OnEnable()
        {
            if (_inventoryService.GetContainer() == null) return;
            _provider = _inventoryService.GetContainer();
            _inventoryManager.SetContainer(_provider);
            if (!_provider.isLootSpawned) _inventoryManager.SpawnRandomLoot(_provider.Config);
            renderer.SetInventory(_inventoryManager,_provider.InventoryRenderMode);
        }

        private void OnDisable()
        {
            _provider = null;
        }
    }
}