using UnityEngine;

namespace LevelGeneration
{
    public class LevelDesigner : MonoBehaviour
    {
        [Header("LEVEL GENERATION")]
        //параметры LevelGenerator
        [SerializeField] private LevelGenerator.LevelType _levelType;
        [Range(1,5)]
        [SerializeField] private int levelSize;
        [SerializeField] private int bossRoomsCount;
        
        //параметры doorSpawner
        [SerializeField] private bool hasElectricDoors;

        //параметры ElectricSpawner
        [SerializeField] private bool hasElectricPanel;
        [SerializeField] private bool hasGenerator;
        [Range(10,50)]
        [SerializeField] private float lightOverloadLevel;
        [Range(1,5)]
        [SerializeField] private float darknessLevel;

        //Параметры EventSpawner
        private int jumpscareCount;
        
        //параметры NPCSpawner
        private bool hasRootBoss;
        
        
        private LevelBlueprint _levelBlueprint;
        
        
        private void CreateLevelBluePrint()
        {
            _levelBlueprint = new LevelBlueprint();
            _levelBlueprint.LevelType = _levelType;

        }
    }
}