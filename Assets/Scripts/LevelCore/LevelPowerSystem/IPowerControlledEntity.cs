namespace LiftGame.LevelCore.LevelPowerSystem
{
    public interface IPowerControlledEntity
    {
        void ConnectToPowerService();
        void DisconnectFromPowerService();
        void PowerUp();
        void PowerDown();
    }
}