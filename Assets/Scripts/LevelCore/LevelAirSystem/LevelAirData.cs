using UnityEngine;

namespace LiftGame.LevelCore.LevelAirSystem
{
    public class LevelAirData : ScriptableObject
    {
        public bool IsAirEnabled { get; set; }

        public void ResetData()
        {
            IsAirEnabled = false;
        }
    }
}