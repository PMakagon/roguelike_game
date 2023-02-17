using LiftGame.Inventory.Core;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.Case
{
    public class CasePanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer inventoryRenderer;
        [SerializeField] private GameObject casePanel;
        private CaseItemRepository _repository;
        private bool _isPreviouslyInRange = false;
        private IPlayerInventoryService _inventoryService;


        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void Start()
        {
            casePanel.SetActive(false);
        }
        

        private void OnEnable()
        {
            if (_repository == null) return;
            _isPreviouslyInRange = _repository.IsInRange;
            inventoryRenderer.RenderInventory(_inventoryService.GetCaseRepositoryManager(),_repository.InventoryRenderMode);
        }

        private void Update()
        {
            if (_repository == null) return;
            if (_repository.IsInRange != _isPreviouslyInRange)
            {
               casePanel.SetActive(_repository.IsInRange);
               _isPreviouslyInRange = _repository.IsInRange;
            }
        }

    }
}