using System;
using TMPro;
using UnityEngine;

namespace LiftGame.Ui.Frontend
{
    public class MouseMenuButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Sprite buttonBackGround;
        [SerializeField] private Sprite buttonSelection;
        private Action _onButtonClick;

        public void RenderButton(Action action)
        {
            
        }
        
    }
}