using System.Collections.Generic;
using LiftGame.PlayerCore;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [Header("INTERACTABLE SETTINGS")] 
        [BoxGroup()][SerializeField] private bool isInteractable = true;
        [BoxGroup()][SerializeField] private string tooltipMessage = "Interact";
        public IPlayerData CachedPlayerData { get; set; }
        public List<Interaction> Interactions { get; } = new List<Interaction>();
        public bool IsInteractable => isInteractable;

        public virtual string TooltipMessage
        {
            get => tooltipMessage;
            set => tooltipMessage = value;
        }
        
        protected virtual void Awake()
        {
            CreateInteractions();
            BindInteractions();
            AddInteractions();
        }
        
        public virtual void CreateInteractions()
        {
            
        }

        public virtual void BindInteractions()
        {
            
        }
        
        public virtual void AddInteractions()
        {
            
        }

        private void CheckInteractions()
        {
            foreach (var interaction in Interactions)
            {
                interaction.CheckIsExecutable(CachedPlayerData);
            }
        }

        private void ResetCache()
        {
            CachedPlayerData = null;
        }

        public virtual void PreInteract(IPlayerData playerData)
        {
            CachedPlayerData = playerData;
            CheckInteractions();
            // Debug.Log("PRE_INTERACTED: " + gameObject.name);
        }

        public virtual void OnInteract(Interaction interaction)
        {
            CheckInteractions();
            // Debug.Log("INTERACTED: " + gameObject.name);
        }

        public void PostInteract()
        {
            ResetCache();
        }
    }
}