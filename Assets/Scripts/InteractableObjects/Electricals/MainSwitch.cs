using System;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.LevelCore.LevelPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.InteractableObjects.Electricals
{
    public class MainSwitch : Interactable, IPowerControlledEntity
    {
        [SerializeField] private bool isSwitchedOn;
        [SerializeField] private Transform button;
        private Interaction _toSwitch = new Interaction("Toogle", true);
        
        private const string OFF = "Turn OFF";
        private const string ON = "Turn ON";
        private ILevelPowerService _levelPowerService;

        [Inject]
        private void Construct(ILevelPowerService levelPowerService)
        {
            _levelPowerService = levelPowerService;
        }
        
        private void Start()
        {
            ConnectToPowerService();
            SetInteractionLabel();
        }

        private void OnDestroy()
        {
            DisconnectFromPowerService();
        }

        private void SetInteractionLabel()
        {
            _toSwitch.Label = isSwitchedOn ? OFF : ON;
        }

        public bool IsSwitchedOn
        {
            get => isSwitchedOn;
            set => isSwitchedOn = value;
        }

        public override void BindInteractions()
        {
            _toSwitch.actionOnInteract = SwitchPower;
        }
        
        public override void AddInteractions()
        {
            Interactions.Add(_toSwitch);
        }

        private bool SwitchPower()
        {
            isSwitchedOn = !isSwitchedOn;
            SetInteractionLabel();
            if (isSwitchedOn)
            {
                button.Rotate(0.0f, 0.0f, +100.0f, Space.Self);
                PowerUp();
            }
            else
            {
                button.Rotate(0.0f, 0.0f, -100.0f, Space.Self);
                PowerDown();
            }
            return true;
        }

        public void ConnectToPowerService()
        {
            _levelPowerService.AddPowerControlledEntity(this);
        }

        public void DisconnectFromPowerService()
        {
            _levelPowerService.RemovePowerControlledEntity(this);
        }

        public void PowerUp()
        {
            _levelPowerService.PowerUpConnectedLoad();
        }

        public void PowerDown()
        {
            _levelPowerService.PowerDownConnectedLoad();
        }
    }
}