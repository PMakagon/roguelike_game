using LiftGame.FPSController.InteractionSystem.InteractionMenu;
using LiftGame.Ui;
using LiftGame.Ui.HUD;
using UnityEngine;
using Zenject;

namespace LiftGame.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private InteractionMenu interactionMenu;
        
        
        public override void InstallBindings()
        {
            Container.Bind<InteractionMenu>().FromInstance(interactionMenu).Lazy();
        }
        
    }
}