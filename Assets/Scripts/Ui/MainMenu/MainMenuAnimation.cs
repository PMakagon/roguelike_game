using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LiftGame.Ui.MainMenu
{
    public class MainMenuAnimation : MonoBehaviour
    {
        [SerializeField] private Animator[] doorAnimators;
        private static readonly int IsOpened = Animator.StringToHash("IsOpened");

        public void PlayOpenAnimation()
        {
            foreach (var animator in doorAnimators)
            {
                animator.SetBool(IsOpened,true);
            }
        }

        public void PlayCloseAnimation()
        {
            foreach (var animator in doorAnimators)
            {
                animator.SetBool(IsOpened,false);
            }
        }
    }
}