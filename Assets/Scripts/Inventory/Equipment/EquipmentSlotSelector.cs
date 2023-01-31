using LiftGame.GameCore.Input.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiftGame.Inventory.Equipment
{
    public class EquipmentSlotSelector : MonoBehaviour
    {
        [SerializeField] private Image leftSelector;
        [SerializeField] private Image rightSelector;
        private IPlayerInventoryService _inventoryService;

        //MonoBehaviour injection
        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        
        private void Start()
        {
            _inventoryService.OnInventoryLoad += UpdateSlotSelection;
            _inventoryService.OnInventoryOpen += UpdateSlotSelection;
            EquipmentInputData.OnSwitchWeaponPressed += UpdateSlotSelection;
        }

        private void OnDestroy()
        {
            _inventoryService.OnInventoryLoad -= UpdateSlotSelection;
            _inventoryService.OnInventoryOpen -= UpdateSlotSelection;
           EquipmentInputData.OnSwitchWeaponPressed -= UpdateSlotSelection;
        }
        
        private void UpdateSlotSelection()
        {
            leftSelector.gameObject.SetActive(_inventoryService.GetEquipmentRepository()[0].IsSelected);
            rightSelector.gameObject.SetActive(_inventoryService.GetEquipmentRepository()[1].IsSelected);
        }
        
    }
}