using System;
using UnityEngine;

namespace LevelGeneration
{
    public class DifficultyController : MonoBehaviour
    {
        [SerializeField] private LevelDesigner levelDesigner;
        public int LevelCount { get; set; } 
        public bool EnableDifficultySetting { get; set; }
        public bool isDisabled;

        private void Update()
        {
            if (EnableDifficultySetting)
            {
                EnableDifficultySetting = false;
                SetDifficulty();
            }
        }

        private void SetDifficulty()
        {
            if (!isDisabled)
            {
                levelDesigner.LevelType = SetLevelType();
                levelDesigner.DarknessLevel = SetDarknessLevel();
            }
        }

        private LevelGenerator.LevelType SetLevelType()
        {
            if (LevelCount<10)
            {
                return LevelGenerator.LevelType.Dorm;
            }
            return LevelGenerator.LevelType.Dorm;
        }

        private float SetDarknessLevel()
        {
            float darkness = 1;
            if (SetLevelType()==LevelGenerator.LevelType.Dorm)
            {
                return darkness /= LevelCount;
            }
            return darkness;
        }
    }
}