using LiftGame.FPSController.InteractionSystem;
using LiftGame.LiftStateMachine;
using LiftGame.PlayerCore;
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
        
        public override void OnInteract(IPlayerData playerData)
        {
            // var inventory=  playerData.GetInventoryData().InventoryContainer;
            // foreach (var item in inventory.Items)
            // {
            //     if (item.ItemType == ItemType.Quest)
            //     {
            //         inventory.RemoveItem(item);
            //         textBox.text = "DONE";
            //         Debug.Log("Quest completed");
            //         LiftControllerData.OnLevelGameLoopFinished.Invoke();
            //         return;
            //     }
            // }
            // textBox.text = "kek";
            // Debug.Log("Item not found");
        }
    }
}