using LiftGame.Inventory.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.Container
{
    public class ContainerPanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer inventoryRenderer;
        [SerializeField] private TextMeshProUGUI containerLabel;
        private ContainerItemRepository _repository;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        
        private void Awake()
        {
            inventoryRenderer.SetupRenderer();
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
            containerLabel.text = _inventoryService.InventoryData.CurrentContainer.Config.ContainerName;
            var repoManager = _inventoryService.GetContainerRepositoryManager();
            repoManager.SetContainer(_inventoryService.GetContainerRepository());
            // to move
            if (!_repository.isLootSpawned) repoManager.SpawnRandomLoot(_repository.Config);
            //
            inventoryRenderer.RenderInventory(repoManager,_repository.InventoryRenderMode);
        }

        private void OnDisable()
        {
            _repository = null;
        }
    }
}