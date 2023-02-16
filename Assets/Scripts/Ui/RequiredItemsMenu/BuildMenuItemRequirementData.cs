using System;
using LiftGame.Inventory.Items;
using UnityEngine;

namespace LiftGame.Ui.RequiredItemsMenu
{
    [CreateAssetMenu(fileName = "BuildMenuItemRequirementData", menuName = "NewInteractionSystem/BuildMenuItemRequirementData")]
    public class BuildMenuItemRequirementData : ScriptableObject
    {
        [SerializeField] private ItemDefinition requiredItem;
        [SerializeField] private int requiredAmount;
        [SerializeField]private bool removeOnConfirm = true;
        private int _currentAmount=0;
        public bool IsFulfilled { get; set; }

        public void Create(ItemDefinition item,int currentAmount)
        {
            requiredItem = item;
            _currentAmount = currentAmount;
        }

        private void OnEnable()
        {
            ResetData();
        }

        private void ResetData()
        {
            _currentAmount = 0;
            IsFulfilled = false;
        }

        public string CounterText => _currentAmount + "/" + requiredAmount;

        public ItemDefinition RequiredItem => requiredItem;

        public int RequiredAmount => requiredAmount;

        public bool RemoveOnConfirm => removeOnConfirm;

        public int CurrentAmount
        {
            get => _currentAmount;
            set => _currentAmount = value;
        }
    }
}