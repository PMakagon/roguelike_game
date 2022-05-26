using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class LevelGenerationManager : MonoBehaviour

    {
        [SerializeField] private LevelChanger levelChanger;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private DoorSpawner doorSpawner;
        [SerializeField] private ElectricitySpawner electricitySpawner;
        [SerializeField] private LevelDesigner levelDesigner;
        [SerializeField] private DifficultyController difficultyController;
        
        
        
        private LevelBlueprint _levelBlueprint;
        private List<Room> _spawnedRooms;
        private RootRoom _currentRoot;


        // private void Awake()
        // {
        //     levelChanger = GetComponentInChildren<LevelChanger>();
        //     levelGenerator = GetComponentInChildren<LevelGenerator>();
        //     doorSpawner = GetComponentInChildren<DoorSpawner>();
        //     electricitySpawner=GetComponentInChildren<ElectricitySpawner>();
        //     levelDesigner = GetComponentInChildren<LevelDesigner>();
        // }
        
        private void GetLevel()
        {
            _currentRoot = levelGenerator.CurrentRoot;
            _spawnedRooms = levelGenerator.SpawnedRooms;
            
            _levelBlueprint.CurrentRoot = levelGenerator.CurrentRoot;
            _levelBlueprint.SpawnedRooms = levelGenerator.SpawnedRooms;
            levelGenerator.showReport = true;
        }

        [ContextMenu("DESTROY LEVEL")] 
        private void DestroyLevel()
        {
            Destroy(_currentRoot.gameObject);
            _spawnedRooms.Clear();
        }

        [ContextMenu("RUN TEST")] 
        private void Run()
        {
            StartCoroutine(RunTest());
        }

        public IEnumerator RunTest()
        {
            levelDesigner.CreateBlueprint = true;
            _levelBlueprint = levelDesigner.LevelBlueprint;
            
            levelChanger.UpdateCode = true;
            levelChanger.SwitchLevel=true;
            
            levelGenerator.LevelBlueprint = _levelBlueprint;
            levelGenerator.enableGeneration = true;
            while (!levelGenerator.LevelReady)
            {
                yield return null;
            }
            GetLevel();
            yield return null;
            levelGenerator.ResetLevelGenerator();
            doorSpawner.SpawnedRooms = _spawnedRooms;
            doorSpawner.EnableDoorSpawner = true;
            yield return null;
            electricitySpawner.LevelBlueprint = _levelBlueprint;
            electricitySpawner.enableElectricityGeneration = true;
        }

        public LevelBlueprint LevelBlueprint
        {
            get => _levelBlueprint;
            set => _levelBlueprint = value;
        }
    }
}