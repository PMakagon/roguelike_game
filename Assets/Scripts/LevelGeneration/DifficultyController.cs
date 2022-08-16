using System;
using UnityEngine;

namespace LevelGeneration
{
    public class DifficultyController : MonoBehaviour
    {
        [SerializeField] private LevelDesigner levelDesigner;
        public int LevelCount { get; set; }
        public bool IsDifficultySettingEnabled { get; set; }

        private void Update()
        {
            if (IsDifficultySettingEnabled)
            {
                IsDifficultySettingEnabled = false;
                LevelCount++;
                SetDifficulty();
            }
        }

        private void SetDifficulty()
        {
            if (levelDesigner.IsAuto)
            {
                levelDesigner.LevelType = SetLevelType();
                levelDesigner.DarknessLevel = SetDarknessLevel();
                levelDesigner.LevelSize = SetLevelSize();
            }
        }

        private LevelGenerator.LevelType SetLevelType()
        {
            if (LevelCount < 10)
            {
                return LevelGenerator.LevelType.Dorm;
            }

            return LevelGenerator.LevelType.Dorm;
        }

        private float SetDarknessLevel()
        {
            float darkness = 1;
            if (SetLevelType() == LevelGenerator.LevelType.Dorm)
            {
                darkness /= LevelCount;
                return darkness;
            }

            return darkness;
        }

        private int SetLevelSize()
        {
            if (LevelCount < 2)
            {
                return 1;
            }

            if (LevelCount > 3 && LevelCount < 5)
            {
                return 2;
            }

            if (LevelCount > 5 && LevelCount < 8)
            {
                return 3;
            }

            if (LevelCount >= 8)
            {
                return 4;
            }

            return 1;
        }
    }
}