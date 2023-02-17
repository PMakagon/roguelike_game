using TMPro;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem.InteractionUI
{
    public class InteractionTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tooltipText;
        
        public void SetTooltip(string tooltip)
        {
            tooltipText.SetText(tooltip);
        }
        public void ResetTooltip()
        {
            tooltipText.SetText("");
        }

        private void OnDisable()
        {
            ResetTooltip();
        }
    }
}