using System;
using LiftGame.LevelCore.LevelAirSystem;
using LiftGame.LevelCore.LevelData;

namespace LiftGame.ProxyEventHolders.Level
{
    public class LevelAirEventHolder
    {
        public static event Action<LevelAirData> OnAirSystemOn;
        public static event Action<LevelAirData> OnAirSystemOff;
    }
}