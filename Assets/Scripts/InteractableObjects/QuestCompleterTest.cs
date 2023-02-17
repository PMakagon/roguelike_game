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
        private Interaction _toPutItem = new Interaction("Put Item", true);

        protected override void Awake()
        {
            base.Awake();
            LiftControllerData.OnLevelGameLoopFinished += ChangeText;
        }

        private void ChangeText()
        {
            textBox.text = "bring more";
        }
        
        public override void BindInteractions()
        {
            _toPutItem.actionOnInteract = CompleteQuest;
        }
        
        public override void AddInteractions()
        {
            Interactions.Add(_toPutItem);
        }
        private bool CompleteQuest()
        {
            var inventory = CachedServiceProvider.InventoryService;
            foreach (var item in inventory.GetAllItems().Where(item => ((ItemDefinition)item).ItemType == ItemType.Quest))
            {
                inventory.TryToRemoveItem(item);
                textBox.text = "DONE";
                LiftControllerData.OnLevelGameLoopFinished?.Invoke();
                return true;
            }
            textBox.text = "kek";
            return false;
        }
        
    }
}