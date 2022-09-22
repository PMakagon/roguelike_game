using System;
using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{
    [CreateAssetMenu(fileName = "UIInputData", menuName = "PlayerInputData/UIInputData")]
    public class UIInputData : ScriptableObject
    {
        
        private bool _inventoryClicked;
        private bool _inventoryReleased;

        public static event Action OnInventoryClicked = delegate { };

        public void UpdateInputEvents()
        {
            if (_inventoryClicked) OnInventoryClicked?.Invoke();
        }
        
        public bool InventoryClicked
        {
            get => _inventoryClicked;
            set => _inventoryClicked = value;
        }

        public bool InventoryReleased
        {
            get => _inventoryReleased;
            set => _inventoryReleased = value;
        }
        
        public void ResetInput()
        {
            _inventoryClicked = false;
            _inventoryReleased = false;
        }
    }
}