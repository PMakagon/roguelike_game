using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
        private RootRoom _currentRoot;
        private RootRoom _previousLevel;

        public bool StartGeneration { get; set; }
        public bool destroyPrevLevel=true;


        // private void Awake()
        // {
        //     levelChanger = GetComponentInChildren<LevelChanger>();
        //     levelGenerator = GetComponentInChildren<LevelGenerator>();
        //     doorSpawner = GetComponentInChildren<DoorSpawner>();
        //     electricitySpawner=GetComponentInChildren<ElectricitySpawner>();
        //     levelDesigner = GetComponentInChildren<LevelDesigner>();
        // }

        private void Update()
        {
            if (StartGeneration)
            {
                StartGeneration = false;
                StartCoroutine(RunGeneration());
            }
        }

        private IEnumerator RunGeneration()
        {
            if (destroyPrevLevel)
            {
                Invoke(nameof(DestroyLevel),10);
            }
            difficultyController.IsDifficultySettingEnabled = true;
            levelDesigner.CreateBlueprint = true;
            yield return null;
            _levelBlueprint = levelDesigner.LevelBlueprint; //объеденить
            levelGenerator.LevelBlueprint = _levelBlueprint; //
            levelGenerator.enableGeneration = true;
            while (!levelGenerator.LevelReady)
            {
                yield return null;
            }
            GetLevel();
            yield return null;
            levelGenerator.ResetLevelGenerator();
            //ну какая то шляпа конечно
            doorSpawner.LevelBlueprint = _levelBlueprint;
            doorSpawner.EnableDoorSpawner = true;
            electricitySpawner.LevelBlueprint = _levelBlueprint;
            electricitySpawner.isElectricityGenerationEnabled = true;
        }

        private void GetLevel()
        {
            if (_currentRoot)
            {
                _previousLevel = _currentRoot;
            }
            _currentRoot = levelGenerator.CurrentRoot;
            _levelBlueprint.CurrentRoot = levelGenerator.CurrentRoot;
            _levelBlueprint.SpawnedRooms = levelGenerator.SpawnedRooms;
            levelGenerator.showReport = true;
        }
        
        private void DestroyLevel()
        {
            if (_previousLevel && _currentRoot!=_previousLevel)
            {
                Destroy(_previousLevel.gameObject);
            }
        }

        [ContextMenu("RUN TEST")]
        private void Run()
        {
            StartCoroutine(RunTest());
        }

        public IEnumerator RunTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (destroyPrevLevel)
            {
                Invoke(nameof(DestroyLevel),5);
            }
            levelChanger.UpdateCode = true;
            levelChanger.TestSwitchLevel = true;
            difficultyController.IsDifficultySettingEnabled = true;
            levelDesigner.CreateBlueprint = true;
            yield return null;
            _levelBlueprint = levelDesigner.LevelBlueprint; //объеденить
            levelGenerator.LevelBlueprint = _levelBlueprint; //
            levelGenerator.enableGeneration = true;
            while (!levelGenerator.LevelReady)
            {
                yield return null;
            }
            GetLevel();
            yield return null;
            levelGenerator.ResetLevelGenerator();
            //ну какая то шляпа конечно
            doorSpawner.LevelBlueprint = _levelBlueprint;
            doorSpawner.EnableDoorSpawner = true;
            electricitySpawner.LevelBlueprint = _levelBlueprint;
            electricitySpawner.isElectricityGenerationEnabled = true;
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Debug.Log("RunTime " + elapsedTime);
        }

        public LevelBlueprint LevelBlueprint
        {
            get => _levelBlueprint;
            set => _levelBlueprint = value;
        }
    }
}