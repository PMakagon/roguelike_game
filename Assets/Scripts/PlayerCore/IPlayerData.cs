using LiftGame.Inventory;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerPowerSystem;

namespace LiftGame.PlayerCore
{
    public interface IPlayerData
    {
        InventoryData GetInventoryData();
        PlayerHealthData GetHealthData();
        PlayerMentalData GetMentalData();
        PlayerPowerData GetPowerData();
    }
}