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

        private List<Room> _spawnedRooms;
        private RootRoom _currentRoot;

        // private void Awake()
        // {
        //     levelChanger = GetComponentInChildren<LevelChanger>();
        //     levelGenerator = GetComponentInChildren<LevelGenerator>();
        //     doorSpawner = GetComponentInChildren<DoorSpawner>();
        // }

        private void GetLevel()
        {
            _currentRoot = levelGenerator.CurrentRoot;
            _spawnedRooms = levelGenerator.SpawnedRooms;
            levelGenerator.showReport = true;
            // levelGenerator.ResetLevelGenerator();
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
            levelChanger.UpdateCode = true;
            levelChanger.SwitchLevel=true;
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
            electricitySpawner.CurrentRoot = _currentRoot;
            electricitySpawner.SpawnedRooms = _spawnedRooms;
            yield return null;
            electricitySpawner.enableElectricityGeneration = true;
        }
        
        
    }
}