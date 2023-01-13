using System;
using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{
    [CreateAssetMenu(fileName = "UIInputData", menuName = "PlayerInputData/UIInputData")]
    public class UIInputData : ScriptableObject
    {
        public static event Action OnInventoryClicked = delegate { };
        public static event Action<float> OnScrolling = delegate { };
        public float ScrollWheelDirection { get; set; }
        public bool InventoryClicked { get; set; }

        public bool InventoryReleased { get; set; }

        public void UpdateInputEvents()
        {
            if (InventoryClicked) OnInventoryClicked?.Invoke();
            if (ScrollWheelDirection != 0) OnScrolling?.Invoke(ScrollWheelDirection);
        }

        public void ResetInput()
        {
            InventoryClicked = false;
            InventoryReleased = false;
        }
    }
}