using System;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.NewInventory.Case
{
    [RequireComponent(typeof(BoxCollider))]
    public class CaseInteractableContainer : Interactable
    {
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] private EquipableContainerConfig caseConfig;
        private CaseItemRepository _caseRepository;
        private bool _isBinded;

        private void Awake()
        {
            TooltipMessage = "Bind to"  + caseConfig.ContainerName;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isBinded) return;
            if (!other.CompareTag("Player")) return;
            inventoryData.CaseRepository.IsInRange = true;
            Debug.Log("CASE");
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isBinded) return;
            if (!other.CompareTag("Player")) return;
            inventoryData.CaseRepository.IsInRange = false;
            Debug.Log("NO CASE");
        }

        public override void OnInteract(IPlayerData playerData)
        {
            _caseRepository ??= new CaseItemRepository(caseConfig.Widht, caseConfig.Height);
            if (!_isBinded)
            {
                inventoryData.CaseRepository = _caseRepository;
                inventoryData.CaseConfig = caseConfig;
                TooltipMessage = "Unbind from" + caseConfig.ContainerName;
                _isBinded = true;
                _caseRepository.IsBinded = _isBinded;
            }
            else
            {
                TooltipMessage = "Bind to" + caseConfig.ContainerName;
                inventoryData.CaseRepository = null;
                inventoryData.CaseConfig = null;
                _isBinded = false;
                _caseRepository.IsBinded = _isBinded;
            }
        }
    }
}