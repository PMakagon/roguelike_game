using LiftGame.NewInventory;
using LiftGame.NewInventory.Items;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    // to refactor
    public class EquipmentSwitcher : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        private IPlayerInventoryService _inventoryService;
        private Animator _currentEquipmentAnimator;
        private int _activeSlotID;
        private float _switchDelay;
        private readonly PlayerEquipmentWorldView[] _spawnedEquipments = new PlayerEquipmentWorldView[2] { null, null };

        public void Initialize(IPlayerInventoryService playerInventoryService, IPlayerPowerService playerPowerService)
        {
            _inventoryService = playerInventoryService;
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }

        private void AddListener(int slotId)
        {
            _inventoryService.GetEquipmentRepositoryManager(slotId).OnItemRemovedFromSlot += RemoveEquipmentFromEmptySlot;
        }
        
        private void RemoveAllListeners()
        {
            _inventoryService.GetEquipmentRepositoryManager(0).OnItemRemovedFromSlot -= RemoveEquipmentFromEmptySlot;
            _inventoryService.GetEquipmentRepositoryManager(1).OnItemRemovedFromSlot -= RemoveEquipmentFromEmptySlot;
        }

        // убирает модель рук из пула и уничтожает на сцене при освобобождении слота  
        private void RemoveEquipmentFromEmptySlot(int slotId)
        {
            //убрать из рук если освобождается активный слот
            if (slotId == _activeSlotID)
            {
                //если переключится некуда
                if (_inventoryService.GetEquipmentRepository()[_activeSlotID].IsEmpty) 
                {
                    _spawnedEquipments[_activeSlotID]?.OnUnEquip();
                }
                else
                {
                    SwapCurrentEquipment();
                }
            }
            Destroy(_spawnedEquipments[slotId]?.gameObject,3);
            _spawnedEquipments[slotId] = null;
        }
        
        public void WithdrawEquipment()
        {
            foreach (var repo in _inventoryService.GetEquipmentRepository())
            {
                if (repo.IsEmpty) continue;
                _activeSlotID = repo.SlotId;
                SetSelectionOnSlot(_activeSlotID);
                SwitchCurrentTo(repo.GetEquipmentItem());
                _spawnedEquipments[_activeSlotID].OnEquip();
                return;
            }
        }

        private void SwitchCurrentTo(EquipmentItem equipmentItem)
        {
            if (equipmentItem == null) return;
            var equipmentToSwitch = _inventoryService.GetCurrentEquipment();
            if (equipmentToSwitch != null)
            {
                equipmentToSwitch.OnUnEquip();
                HideEquipment(_activeSlotID);
                _activeSlotID = _activeSlotID == 0 ? 1 : 0;
                SetSelectionOnSlot(_activeSlotID);
            }
            _inventoryService.SetCurrentEquipment(SpawnEquipment(equipmentItem.EquipmentPrefab));
        }

        private PlayerEquipmentWorldView SpawnEquipment(PlayerEquipmentWorldView equipmentWorldView)
        {
            var spawnedEquipment = Instantiate(equipmentWorldView, spawnPoint);
            _spawnedEquipments[_activeSlotID] = spawnedEquipment;
            AddListener(_activeSlotID);
            return spawnedEquipment;
        }

        private void HideEquipment(int slotID)
        {
            _spawnedEquipments[slotID]?.gameObject.SetActive(false);
        }

        public void SwapCurrentEquipment()
        {
            if (_inventoryService.GetCurrentEquipment() == null)
            {
                WithdrawEquipment();
                return;
            }

            var index = _activeSlotID == 0 ? 1 : 0;
            if (_spawnedEquipments[index] == null)
            {
                //смотрим есть ли куда переключить
                if (_inventoryService.GetEquipmentRepository()[index].IsEmpty) return; //некуда
                _spawnedEquipments[_activeSlotID].OnUnEquip();
                HideEquipment(_activeSlotID);
                _activeSlotID = index;
                SetSelectionOnSlot(_activeSlotID);
                _inventoryService.SetCurrentEquipment(SpawnEquipment(_inventoryService.GetEquipmentRepository()[index]
                    .GetEquipmentItem().EquipmentPrefab));
                _spawnedEquipments[index].OnEquip();
                return;
            }
            // если искать не надо
            _spawnedEquipments[_activeSlotID].OnUnEquip();
            HideEquipment(_activeSlotID);
            _inventoryService.SetCurrentEquipment(_spawnedEquipments[index]);
            _activeSlotID = index;
            SetSelectionOnSlot(_activeSlotID);
            _spawnedEquipments[index].gameObject.SetActive(true);
            _spawnedEquipments[index].OnEquip();
        }

        private void SetSelectionOnSlot(int activeSlotID)
        {
            var notActiveSlotID = activeSlotID == 0 ? 1 : 0;
            _inventoryService.GetEquipmentRepository()[activeSlotID].IsSelected = true;
            _inventoryService.GetEquipmentRepository()[notActiveSlotID].IsSelected = false;
        }
        
    }
}