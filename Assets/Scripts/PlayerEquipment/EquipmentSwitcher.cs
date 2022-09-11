using System.Collections;
using System.Collections.Generic;
using LiftGame.GameCore.Input.Data;
using LiftGame.InventorySystem;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerEquipment
{
    public class EquipmentSwitcher : MonoBehaviour

    {
        [SerializeField] private EquipmentInputData inputData;
        [SerializeField] private InventoryData _inventoryData;
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
        
        [Inject]
        private void Construct(IPlayerData playerData)
        {
            _inventoryData = playerData.GetInventoryData();
        }
        private void Start()
        {
            _spawnPoint = transform;
            // _inventoryData.InventoryContainer.ClearContainer(); ///test
            _inventoryData.onEquipmentAdd += CheckInventory;
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
            if (_inventoryData.InventoryContainer.HasNothing()) return;
            
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
            if (chosenEquipment==_inventoryData.CurrentEquipment)
            {
                if (_inventoryData.CurrentEquipment.IsEquipped)
                {
                    _inventoryData.CurrentEquipment?.UnEquip();
                }
                else
                {
                    _inventoryData.CurrentEquipment?.Equip();
                }
                _timeSinceLastSwitch = 0f;
                yield break;
            }
            _inventoryData.CurrentEquipment?.UnEquip();
            _inventoryData.CurrentEquipment = chosenEquipment;
            yield return new WaitForSecondsRealtime(2);
            _inventoryData.CurrentEquipment.Equip();
            _currentEquipmentAnimator = _inventoryData.CurrentEquipment.EquipmentAnimator;
            _timeSinceLastSwitch = 0f;
        }
    }
}