using System;
using LiftGame.Inventory.Items;
using LiftGame.PlayerCore;

namespace LiftGame.FPSController.InteractionSystem
{
    public class Interaction
    {
        public string Label { get; set; }
        public InteractionRequirement Requirement { get; }

        public float HoldDuration { get; }
        public bool IsHoldInteract { get; }

        public EquipmentConfig EquipmentConfig { get; }

        public bool IsShouldBeTurnedOn { get; }
        
        public bool IsEquipmentUseNeeded { get; }

        public ItemDefinition ItemDefinition { get; }

        public bool RemoveOnInteract { get; }
        
        public Func<bool> actionOnInteract;


        public Func<bool> ActionOnInteract
        {
            get => actionOnInteract;
            set => actionOnInteract = value;
        }

        public bool IsEnabled { get; set; }

        public bool IsExecutable { get; private set; }


        public Interaction(InteractionConfig config,bool isEnabled)
        {
            Label = config.Label;
            Requirement = config.Requirement;
            IsHoldInteract = config.IsHoldInteract;
            HoldDuration = config.HoldDuration;
            EquipmentConfig = config.EquipmentConfig;
            IsShouldBeTurnedOn = config.IsShouldBeTurnedOn;
            IsEquipmentUseNeeded = config.IsEquipmentUseNeeded;
            ItemDefinition = config.ItemDefinition;
            RemoveOnInteract = config.RemoveOnInteract;
            IsEnabled = isEnabled;
        }

        public Interaction(string name,bool isEnabled)
        {
            Label = name;
            Requirement = InteractionRequirement.None;
            IsHoldInteract = false;
            HoldDuration = 0f;
            EquipmentConfig =null;
            IsShouldBeTurnedOn = false;
            IsEquipmentUseNeeded =false;
            ItemDefinition = null;
            RemoveOnInteract = false;
            IsEnabled = isEnabled;
        }
        public Interaction(string name,float holdDuration,bool isEnabled)
        {
            Label = name;
            Requirement = InteractionRequirement.None;
            IsHoldInteract = true;
            HoldDuration = holdDuration;
            EquipmentConfig =null;
            IsShouldBeTurnedOn = false;
            ItemDefinition = null;
            RemoveOnInteract = false;
            IsEnabled = isEnabled;
        }

        public void CheckIsExecutable(IPlayerData playerData)
        {
            if (!IsEnabled)
            {
                IsExecutable = false;
                return;
            }

            switch (Requirement)
            {
                case InteractionRequirement.None:
                    IsExecutable = true;
                    break;
                case InteractionRequirement.Equipment:
                {
                    var currentEquipment = playerData.GetInventoryData().CurrentEquipment;
                    if (currentEquipment == null)
                    {
                        IsExecutable = false;
                        break;
                    }

                    if (currentEquipment.EquipmentConfig.ID != EquipmentConfig.ID)
                    {
                        IsExecutable = false;
                        break;
                    }

                    if (currentEquipment.EquipmentConfig.IsPowerEquipment && IsShouldBeTurnedOn)
                    {
                        IsExecutable = currentEquipment.EquipmentData.IsOn;
                        break;
                    }

                    IsExecutable = true;
                    break;
                }
                case InteractionRequirement.InventoryItem:
                    // var currentEquipment = playerData.GetInventoryData().GetItemByName()
                    IsExecutable = true;
                    break;
                default:
                    IsExecutable = false;
                    break;
            }
        }
    }
}