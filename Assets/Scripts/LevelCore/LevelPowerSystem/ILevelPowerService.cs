namespace LiftGame.LevelCore.LevelPowerSystem
{
    public interface ILevelPowerService
    {
        void EnableService();
        void DisableService();
        void PowerUpConnectedLoad();
        void PowerDownConnectedLoad();
        void AddPowerControlledEntity(IPowerControlledEntity entity);
        void RemovePowerControlledEntity(IPowerControlledEntity entity);
    }
}