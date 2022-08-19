using System.Collections.Generic;
using LiftGame.InventorySystem.Items;
using UnityEngine;

namespace LiftGame.Ui.Frontend
{
    public class InventoryMouseMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform panelPrefab;
        [SerializeField] private RectTransform menuButtonPrefab;
        private List<RectTransform> _displayedButtons;


        private void RenderMenu(IItem itemInSlot)
        {
            var newPanel =  Instantiate(panelPrefab, Input.mousePosition, Quaternion.identity);
           
        }

        private void CreateButton()
        {
            
        }
        
    }
}