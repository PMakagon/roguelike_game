using LiftGame.LevelCore.LevelData;
using LiftGame.LevelCore.LevelPowerSystem;
using Zenject;

namespace LiftGame.LevelCore.LevelAirSystem
{
    public class LevelAirService : ILevelAirService, IPowerControlledEntity
    {
        private LevelAirData _levelAirData;
        private LevelPowerService _levelPowerService;
        private bool _isPolluted;

        [Inject]
        public LevelAirService(ILevelData levelData,LevelPowerService powerService)
        {
            _levelAirData = levelData.GetAirData();
            _levelPowerService = powerService;
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
            _levelAirData.IsAirEnabled = true;
        }
        
        public void PowerDown()
        {
            _levelAirData.IsAirEnabled = false;
        }
    }
}