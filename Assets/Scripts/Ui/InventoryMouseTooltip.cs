using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui
{
    public class InventoryMouseTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private int wrapLimit;
        public LayoutElement layoutElement;
        

        public void SetTooltip(IInventoryItem item)
        {
            SetHeader((item as ItemDefinition)?.Name);
            SetTooltipContent((item as ItemDefinition)?.Description);
        }

        private void SetHeader(string headerText)
        {
            if (string.IsNullOrEmpty(headerText)) return;
            header.text = headerText;
            header.gameObject.SetActive(true);
        }

        private void SetTooltipContent(string contentText)
        {
            if (string.IsNullOrEmpty(contentText))return;
            layoutElement.enabled = contentText.Length > wrapLimit;
            description.text = contentText;
            description.gameObject.SetActive(true);
        }
        
    }
}