using LiftGame.LevelCore.LevelAirSystem;
using UnityEngine;

namespace LiftGame.LevelCore.LevelData
{
    public class LevelData : ScriptableObject, ILevelData
    {
        [SerializeField] private LevelAirData levelAirDataData;
       

        public void ResetData()
        {
            levelAirDataData.ResetData();
        }

        public LevelAirData GetAirData()
        {
            return levelAirDataData;
        }
    }
}