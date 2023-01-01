using System;
using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{
    [CreateAssetMenu(fileName = "UIInputData", menuName = "PlayerInputData/UIInputData")]
    public class UIInputData : ScriptableObject
    {
        public static event Action OnInventoryClicked = delegate { };

        public bool InventoryClicked { get; set; }

        public bool InventoryReleased { get; set; }

        public void UpdateInputEvents()
        {
            if (InventoryClicked) OnInventoryClicked?.Invoke();
        }

        public void ResetInput()
        {
            InventoryClicked = false;
            InventoryReleased = false;
        }
    }
}