﻿using LiftGame.Ui;
using LiftGame.Ui.HUD;
using UnityEngine;
using Zenject;

namespace LiftGame.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private InteractionUIPanel interactionUIPanel;
        
        
        public override void InstallBindings()
        {
            Container.Bind<InteractionUIPanel>().FromInstance(interactionUIPanel).Lazy();
        }
        
    }
}