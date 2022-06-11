using System;
using UnityEngine;

namespace LevelGeneration
{
    public class LevelDesigner : MonoBehaviour
    {
        private LevelBlueprint _levelBlueprint;
        [SerializeField] private bool isAuto;

        [Header("LEVEL GENERATION")]
        //параметры LevelGenerator
        [SerializeField] private LevelGenerator.LevelType levelType;

        [Range(1, 4)] [SerializeField] private int levelSize;
        [SerializeField] private int bossRoomsCount;


        //параметры doorSpawner
        [SerializeField] private bool hasElectricDoors;


        //параметры ElectricSpawner
        [SerializeField] private bool hasElectricPanel;
        [SerializeField] private bool hasGenerator;
        [Range(10, 50)] [SerializeField] private float lightOverloadLevel;
        [Range(1, 0.1f)] [SerializeField] private float darknessLevel = 1;


        //Параметры EventSpawner
        private int jumpscareCount;


        //параметры NPCSpawner
        private bool hasRootBoss;
        private bool createBlueprint;


        private void Update()
        {
            if (createBlueprint)
            {
                createBlueprint = false;
                // CreateLevelBluePrint();
                _levelBlueprint = new LevelBlueprint(levelType, levelSize, bossRoomsCount, hasElectricDoors, hasGenerator,
                    lightOverloadLevel, darknessLevel, hasElectricPanel, jumpscareCount, hasRootBoss);
            }
        }

        [ContextMenu("CREATE BLUEPRINT")]
        private void CreateLevelBluePrint()
        {
            _levelBlueprint = new LevelBlueprint(levelType, levelSize, bossRoomsCount, hasElectricDoors, hasGenerator,
                lightOverloadLevel, darknessLevel, hasElectricPanel, jumpscareCount, hasRootBoss);
        }

        public LevelBlueprint LevelBlueprint
        {
            get => _levelBlueprint;
            set => _levelBlueprint = value;
        }

        public bool CreateBlueprint
        {
            get => createBlueprint;
            set => createBlueprint = value;
        }

        public LevelGenerator.LevelType LevelType
        {
            get => levelType;
            set => levelType = value;
        }

        public int LevelSize
        {
            get => levelSize;
            set => levelSize = value;
        }

        public int BossRoomsCount
        {
            get => bossRoomsCount;
            set => bossRoomsCount = value;
        }

        public float LightOverloadLevel
        {
            get => lightOverloadLevel;
            set => lightOverloadLevel = value;
        }

        public float DarknessLevel
        {
            get => darknessLevel;
            set => darknessLevel = value;
        }

        public int JumpscareCount
        {
            get => jumpscareCount;
            set => jumpscareCount = value;
        }

        public bool IsAuto => isAuto;
    }
}