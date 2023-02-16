using System.Collections.Generic;
using LiftGame.Inventory.Bag;
using LiftGame.Inventory.Case;
using LiftGame.Inventory.Container;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Equipment;
using LiftGame.Inventory.Items;
using LiftGame.Inventory.Pockets;
using LiftGame.Inventory.PowerCellSlots;
using LiftGame.PlayerEquipment;
using ModestTree;
using UnityEngine;

namespace LiftGame.Inventory
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Player/InventorySystem/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        //убрать в отдельный сервис
        [SerializeField] private EquipableContainerConfig bagSlotConfig;
        [SerializeField] private EquipableContainerConfig pocketsConfig;
        private EquipableContainerConfig _caseConfig;

        private ContainerItemRepository _currentContainer;
        private CaseItemRepository _caseRepository;
        private PocketsItemRepository _pockets;
        private EquipmentRepository[] _equipmentSlots;
        private PowerCellSlotRepository[] _powerCellSlots;
        private BagItemRepository _bagRepository;
        private PlayerEquipmentWorldView _currentEquipment;

        private void Awake()
        {
            ResetData();
        }

        public void ResetData()
        {
            // _caseRepository = new CaseItemRepository(caseConfig.Widht, caseConfig.Height);
            _caseRepository = null;
            _pockets = new PocketsItemRepository();
            _equipmentSlots = new EquipmentRepository[2] { new(0), new(1) };
            _powerCellSlots = new PowerCellSlotRepository[3] { new(0), new(1), new(2) };
            _bagRepository = new BagItemRepository(bagSlotConfig.Widht, bagSlotConfig.Height);
            _currentContainer = null;
            _currentEquipment = null;
            Debug.Log("RESET INVENTORY DATA");
        }

        public BagItemRepository BagRepository
        {
            get => _bagRepository;
            set => _bagRepository = value;
        }

        public CaseItemRepository CaseRepository
        {
            get => _caseRepository;
            set => _caseRepository = value;
        }

        public ContainerItemRepository CurrentContainer
        {
            get => _currentContainer;
            set => _currentContainer = value;
        }

        public PocketsItemRepository PocketsRepository
        {
            get => _pockets;
            set => _pockets = value;
        }

        public EquipmentRepository[] EquipmentSlots
        {
            get => _equipmentSlots;
            set => _equipmentSlots = value;
        }

        public PowerCellSlotRepository[] PowerCellSlots
        {
            get => _powerCellSlots;
            set => _powerCellSlots = value;
        }

        public PlayerEquipmentWorldView CurrentEquipment
        {
            get => _currentEquipment;
            set => _currentEquipment = value;
        }

        public EquipableContainerConfig BagSlotConfig
        {
            get => bagSlotConfig;
            set => bagSlotConfig = value;
        }

        public EquipableContainerConfig PocketsConfig
        {
            get => pocketsConfig;
            set => pocketsConfig = value;
        }

        public EquipableContainerConfig CaseConfig
        {
            get => _caseConfig;
            set => _caseConfig = value;
        }
    }
}