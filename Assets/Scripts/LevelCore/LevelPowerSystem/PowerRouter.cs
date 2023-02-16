using LiftGame.InteractableObjects.Electricals;
using UnityEngine;
using Zenject;

namespace LiftGame.LevelCore.LevelPowerSystem
{
    public class PowerRouter : MonoBehaviour, IPowerControlledEntity
    {
        [SerializeField] private MasterSwitcher[] masterSwitchers;
        private ILevelPowerService _levelPowerService;

        [Inject]
        private void Construct(ILevelPowerService levelPowerService)
        {
            _levelPowerService = levelPowerService;
        }

        private void Start()
        {
            ConnectToPowerService();
        }

        private void OnDestroy()
        {
            DisconnectFromPowerService();
        }

        public void ConnectToPowerService()
        {
            foreach (var masterSwitcher in masterSwitchers)
            {
                
            }
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