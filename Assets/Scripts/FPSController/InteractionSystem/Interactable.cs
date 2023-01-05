using LiftGame.PlayerCore;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [Space] [Header("INTERACTABLE SETTINGS")] 
        [SerializeField] private bool isInteractable = true;
        [ShowIf("isInteractable")] 
        [SerializeField] private bool holdInteract;
        [ShowIf("holdInteract")] 
        [SerializeField] private float holdDuration = 1f;
        [SerializeField] private string tooltipMessage = "Interact";


        public bool IsInteractable => isInteractable;
        public bool HoldInteract => holdInteract;
        public float HoldDuration => holdDuration;

        public virtual string TooltipMessage
        {
            get => tooltipMessage;
            set => tooltipMessage = value;
        }

        public virtual void OnInteract(IPlayerData playerData)
        {
            Debug.Log("INTERACTED: " + gameObject.name);
        }
    }
}