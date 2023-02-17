using System.Collections.Generic;
using LiftGame.PlayerCore;

namespace LiftGame.FPSController.InteractionSystem
{    
    public interface IInteractable
    {
        bool IsInteractable { get; }
        string TooltipMessage { get; set; }
        PlayerServiceProvider CachedServiceProvider { get; }
        public List<Interaction> Interactions { get; }

        //Always call base. of following functions when overriding
        //Called on first registration of Interactable
        void PreInteract(PlayerServiceProvider serviceProvider);
        
        //Called on Confirmed Interaction , can be called multiple times
        void OnInteract(Interaction interaction);
        
        //Called on clearing cached Interactable in InteractionController.Dont call base if you dont need cache cleared
        void PostInteract();
    }
}  
