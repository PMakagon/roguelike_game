using System.Collections.Generic;
using LiftGame.PlayerCore;

namespace LiftGame.FPSController.InteractionSystem
{    
    public interface IInteractable
    {
        bool IsInteractable { get; }
        string TooltipMessage { get; set; }
        IPlayerData CachedPlayerData { get; set; }
        public List<Interaction> Interactions { get; }
        void CreateInteractions();
        void BindInteractions();
        void AddInteractions();

        //Always call base. of following functions when overriding
        //Called on first registration of Interactable
        void PreInteract(IPlayerData playerData);
        
        //Called on Confirmed Interaction , can be called multiple times
        void OnInteract(Interaction interaction);
        
        //Called on clearing cached Interactable in InteractionController
        void PostInteract();
    }
}  
