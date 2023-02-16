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
        public PlayerServiceProvider CachedServiceProvider { get; private set; }
        public List<Interaction> Interactions { get; } = new List<Interaction>();
        public bool IsInteractable => isInteractable;

        public virtual string TooltipMessage
        {
            get => tooltipMessage;
            set => tooltipMessage = value;
        }
        
        protected virtual void Awake()
        {
            gameObject.layer = 6;
            CreateInteractions();
            BindInteractions();
            AddInteractions();
        }
        
        public virtual void CreateInteractions() { }

        public virtual void BindInteractions() { }
        
        public virtual void AddInteractions() { }

        private void CheckInteractions()
        {
            foreach (var interaction in Interactions)
            {
                interaction.CheckIsExecutable(CachedServiceProvider);
            }
        }

        private void ResetCache()
        {
            CachedServiceProvider = null;
        }

        public virtual void PreInteract(PlayerServiceProvider serviceProvider)
        {
            CachedServiceProvider = serviceProvider;
            CheckInteractions();
        }

        public virtual void OnInteract(Interaction interaction)
        {
            CheckInteractions();
        }

        public virtual void PostInteract()
        {
            ResetCache();
        }
    }
}