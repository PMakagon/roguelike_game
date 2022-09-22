using System;
using FarrokhGames.Inventory;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.NewInventory.Case
{
    public class CasePanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer renderer;
        [SerializeField] private GameObject casePanel;
        private CaseItemProvider _provider;
        private bool _isPreviouslyInRange = false;
        private IPlayerInventoryService _inventoryService;
        private CaseInventoryManager _inventoryManager;


        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void Start()
        {
            _inventoryService.onInventoryLoad += Init;
            casePanel.SetActive(false);
            
        }

        private void Init()
        {
            if (_inventoryService.GetCase() == null) throw new NullReferenceException("Provider not exist");
            _provider = _inventoryService.GetCase();
            _inventoryManager = new CaseInventoryManager(_provider,6,5);
            
        }

        private void OnEnable()
        {
            if (_provider == null) return;
            _isPreviouslyInRange = _provider.IsInRange;
            renderer.SetInventory(_inventoryManager,_provider.InventoryRenderMode);
        }

        private void Update()
        {
            if (_provider == null) return;
            if (_provider.IsInRange != _isPreviouslyInRange)
            {
               casePanel.SetActive(_provider.IsInRange);
               _isPreviouslyInRange = _provider.IsInRange;
            }
        }

    }
}