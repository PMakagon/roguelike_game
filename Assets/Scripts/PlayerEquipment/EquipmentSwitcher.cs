using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Items;
using PlayerPowerSystem;
using UnityEngine;

namespace PlayerEquipment
{
    public class EquipmentSwitcher : MonoBehaviour

    {
        [SerializeField] private EquipmentInputData inputData;
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] private Battery battery;
        [SerializeField] private Scanner scanner;
        [SerializeField] private float switchDelay;
        [SerializeField] private List<Transform> equipmentTransforms;

        
        private int _activeSlotNumber = 0;
        private int _selectedSlotNumber;
        private int _prevActiveSlotNumber;
        private Transform _spawnPoint;
        private Animator _currentEquipmentAnimator;
        private float _timeSinceLastSwitch;
        
        private void Start()
        {
            _spawnPoint = transform;
            inventoryData.ResetData(); ///test
            inventoryData.onEquipmentAdd += CheckInventory;
            // inventoryData.CurrentEquipment = GetComponentInChildren<Scanner>();
            // inventoryData.Equipments.Insert(0,inventoryData.CurrentEquipment);
            // inventoryData.CurrentEquipment.Equip();
        }
        
        private void Update()
        {
            HandleInput();
            ChooseEquipment();
            _timeSinceLastSwitch += Time.deltaTime;
        }
        private void CheckInventory()
        {
            if (inventoryData.HasNothing()) return;
            
        }


        private void HandleInput()
        { 
            if (inputData.FirstEquipmentClicked)
            {
                _selectedSlotNumber = 1;
            }

            if (inputData.SecondEquipmentClicked)
            {
                _selectedSlotNumber = 2;
            }
        }
        
        private void ChooseEquipment()
        {
            if (_selectedSlotNumber == _activeSlotNumber) return;
            // if ( inventoryData.Equipments.Count == 0 || inventoryData.Equipments.Count < _selectedSlotNumber) return;
            // StartCoroutine(SwapEquipment(inventoryData.Equipments[_selectedSlotNumber]));
            _selectedSlotNumber = _activeSlotNumber;
        }
        
        
        private IEnumerator SwapEquipment(IPlayerEquipment chosenEquipment)
        {
            if (chosenEquipment==null) yield break;
            if (_timeSinceLastSwitch<=switchDelay) yield break;
            if (chosenEquipment==inventoryData.CurrentEquipment)
            {
                if (inventoryData.CurrentEquipment.IsEquipped)
                {
                    inventoryData.CurrentEquipment?.UnEquip();
                }
                else
                {
                    inventoryData.CurrentEquipment?.Equip();
                }
                _timeSinceLastSwitch = 0f;
                yield break;
            }
            inventoryData.CurrentEquipment?.UnEquip();
            inventoryData.CurrentEquipment = chosenEquipment;
            yield return new WaitForSecondsRealtime(2);
            inventoryData.CurrentEquipment.Equip();
            _currentEquipmentAnimator = inventoryData.CurrentEquipment.EquipmentAnimator;
            _timeSinceLastSwitch = 0f;
        }
    }
}