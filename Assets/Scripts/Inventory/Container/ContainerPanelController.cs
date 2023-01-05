using LiftGame.Inventory.Core;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.Container
{
    [RequireComponent(typeof(InventoryRenderer))]
    public class ContainerPanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer inventoryRenderer;
        private ContainerItemRepository _repository;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        
        private void Start()
        {
            _inventoryService.OnInventoryLoad += Init;
        }

        private void OnDestroy()
        {
            _inventoryService.OnInventoryLoad -= Init;
        }

        private void Init()
        {
            
        }
        
        private void OnEnable()
        {
            _repository = _inventoryService.GetContainerRepository();
            if (_repository == null) return;
            var repoManager = _inventoryService.GetContainerRepositoryManager();
            repoManager.SetContainer(_inventoryService.GetContainerRepository());
            if (!_repository.isLootSpawned) repoManager.SpawnRandomLoot(_repository.Config);
            inventoryRenderer.SetInventory(repoManager,_repository.InventoryRenderMode);
        }

        private void OnDisable()
        {
            _repository = null;
        }
    }
}