namespace LiftGame.LevelCore.LevelPowerSystem
{
    public class LevelPowerService : ILevelPowerService
    {
        private LevelPowerData _levelPowerData;
        
        public void EnableService()
        {
            _levelPowerData.IsPowerEnabled = true;
        }

        public void DisableService()
        {
            _levelPowerData.IsPowerEnabled = false;
        }

        public void PowerUpConnectedLoad()
        {
            foreach (var load in _levelPowerData.ControlledEntities)
            {
                load.PowerUp();
            }
        }
        
        public void PowerDownConnectedLoad()
        {
            foreach (var load in _levelPowerData.ControlledEntities)
            {
                load.PowerDown();
            }
        }

        public void AddPowerControlledEntity(IPowerControlledEntity entity)
        {
            _levelPowerData.ControlledEntities.Add(entity);
        }
        
        public void RemovePowerControlledEntity(IPowerControlledEntity entity)
        {
            _levelPowerData.ControlledEntities.Remove(entity);
        }
    }
}