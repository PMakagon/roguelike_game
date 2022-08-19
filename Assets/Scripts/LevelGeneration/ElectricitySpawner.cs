using System.Collections.Generic;
using LiftGame.LightingSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LiftGame.LevelGeneration
{
    public class ElectricitySpawner : MonoBehaviour
    {
        [SerializeField] private ElectricPanel panelPrefab;
        [SerializeField] private MasterSwitcher masterSwitcherPrefab;
        [SerializeField] private SlaveSwitcher slaveSwitcherPrefab;
        [SerializeField] private LightExtended[] lightExtendedPrefabs;
        private LevelBlueprint _levelBlueprint;
        private MasterSwitcher _lightMasterSwitcher;
        private List<LightExtended> _allLights = new List<LightExtended>();
        public int flickerChanceTest = 30;
        public int brokenChanceTest = 10;
        [SerializeField] private AnimationCurve _animationCurve;
        
        public bool isElectricityGenerationEnabled;


        private void Update()
        {
            if (isElectricityGenerationEnabled)
            {
                isElectricityGenerationEnabled = false;
                AssignMasterSwitchers();
                AdjustLevelDarkness();
            }
        }

        private void SetLightsState()
        {
            
            foreach (var lightExtended in _allLights)
            {
                var random = _animationCurve.Evaluate(Random.Range(0,100));

            }
        }

        private void AdjustLevelDarkness()
        {
            foreach (var lightExtended in _allLights)
            {
                lightExtended.Light.intensity *= _levelBlueprint.DarknessLevel;
            }
        }

        private void AssignMasterSwitchers()
        {
            var currentRoot = _levelBlueprint.CurrentRoot;
            var spawnedRooms = _levelBlueprint.SpawnedRooms;
            _allLights.Clear();
            if (currentRoot)
            {
                var mSwitchers = currentRoot.ElectricPanel.MasterSwitchers;
                _lightMasterSwitcher = mSwitchers[Random.Range(0, mSwitchers.Length)];
                foreach (var lightExtended in currentRoot.LightExtended)
                {
                    lightExtended.MasterSwitcher = _lightMasterSwitcher;
                    _allLights.Add(lightExtended);
                }
                foreach (var spawnedRoom in spawnedRooms)
                {
                    foreach (var lightExtended in spawnedRoom.LightExtended)
                    {
                        lightExtended.MasterSwitcher = _lightMasterSwitcher;
                        _allLights.Add(lightExtended);
                    }
                }
            }
        }

        public LevelBlueprint LevelBlueprint
        {
            get => _levelBlueprint;
            set => _levelBlueprint = value;
        }
    }
}