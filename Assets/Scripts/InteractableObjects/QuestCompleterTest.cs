using System.Linq;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.Inventory.Items;
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
            var inventory=  playerData.GetInventoryData();
            foreach (var item in inventory.GetAllItems().Where(item => ((ItemDefinition)item).ItemType == ItemType.Quest))
            {
                inventory.TryToRemoveItem(item);
                textBox.text = "DONE";
                LiftControllerData.OnLevelGameLoopFinished?.Invoke();
                return;
            }
            textBox.text = "kek";
        }
    }
}