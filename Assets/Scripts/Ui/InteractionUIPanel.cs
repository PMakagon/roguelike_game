using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui
{        
    public class InteractionUIPanel : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI tooltipText;
        [SerializeField] private RectTransform canvasTransform;

        private void Start()
        {
            gameObject.SetActive(true);
        }

        public Slider ProgressBar
        {
            get => progressBar;
            set => progressBar = value;
        }

        public TextMeshProUGUI TooltipText
        {
            get => tooltipText;
            set => tooltipText = value;
        }
        
        public void SetPanelActive(bool state)
        {
            canvasTransform.gameObject.SetActive(state);
            
        }
        

        public void SetTooltipActive(bool _state)
        {
            tooltipText.gameObject.SetActive(_state);
            
        }

        public void SetProgressBarActive(bool _state)
        {
            progressBar.gameObject.SetActive(_state);
        }


        public void SetTooltip(string tooltip)
        {
            tooltipText.SetText(tooltip);
        }


        public void UpdateProgressBar(float fillAmount)
        {
            progressBar.value = fillAmount;
        }


        public void ResetUI()
        {
            progressBar.value = 0f;
            tooltipText.SetText("");
            SetTooltipActive(false);
            SetProgressBarActive(false);
        }

        //TEST
        public void SetToolTip(Transform parent , string tooltip, float holdProgress)
        {
            if(parent)
            {
                canvasTransform.position = parent.position;
                canvasTransform.SetParent(parent);
            }

            SetTooltip(tooltip);
            UpdateProgressBar(holdProgress);
        }
        
        public void LookAtPlayer(Transform player)
        {
            canvasTransform.LookAt(player,Vector3.up);
        }
        
        public void UnparentTooltip()
        {
            canvasTransform.SetParent(null);
        }
        // // //
    }
}
