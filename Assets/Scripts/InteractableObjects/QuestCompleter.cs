using System;
using FPSController.Interaction_System;
using InventorySystem;
using InventorySystem.Items;
using LiftStateMachine;
using TMPro;
using UnityEngine;

namespace InteractableObjects
{
    public class QuestCompleter : Interactable
    {
        [SerializeField] private TextMeshPro textBox;

        private void Awake()
        {
            LiftControllerData.OnLevelGameLoopFinished += ChangeText;
        }

        private void ChangeText()
        {
            textBox.text = "bring more";
        }
        
        public override void OnInteract(InventoryData inventoryData)
        {
            foreach (var item in inventoryData.Items)
            {
                if (item.ItemType == ItemType.Quest)
                {
                    inventoryData.RemoveItem(item);
                    textBox.text = "DONE";
                    Debug.Log("Quest completed");
                    LiftControllerData.OnLevelGameLoopFinished.Invoke();
                    return;
                }
            }
            textBox.text = "sosi";
            Debug.Log("Item not found");
        }
    }
}