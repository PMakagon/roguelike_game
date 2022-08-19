using LiftGame.FPSController.InteractionSystem;
using LiftGame.InventorySystem;
using LiftGame.InventorySystem.Items;
using LiftGame.LiftStateMachine;
using TMPro;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class QuestCompleterTest : Interactable
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
            textBox.text = "kek";
            Debug.Log("Item not found");
        }
    }
}