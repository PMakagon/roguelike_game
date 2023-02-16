using System;
using LiftGame.LevelCore.LevelData;
using LiftGame.LevelCore.LevelPowerSystem;

namespace LiftGame.ProxyEventHolders.Level
{
    public class LevelPowerEventHolder
    {
        public static event Action<LevelPowerData> OnLevelPowerOn;
        public static event Action<LevelPowerData> OnLevelPowerOff;
    }
}