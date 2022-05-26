using System;
using System.Collections.Generic;
using LightingSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class ElectricitySpawner : MonoBehaviour
    {
        [SerializeField] private ElectricPanel panelPrefab;
        [SerializeField] private MasterSwitcher masterSwitcherPrefab;
        [SerializeField] private SlaveSwitcher slaveSwitcherPrefab;
        [SerializeField] private LightExtended[] lightExtendedPrefabs;
        private LevelBlueprint _levelBlueprint;
        private MasterSwitcher _lightMasterSwitcher;
        private List<Light> _allLights = new List<Light>();
        
        public bool enableElectricityGeneration;


        private void Update()
        {
            if (enableElectricityGeneration)
            {
                enableElectricityGeneration = false;
                AssignMasterSwitchers();
                AdjustLevelDarkness();
            }
        }

        private void AdjustLevelDarkness()
        {
            foreach (var lightSource in _allLights)
            {
                lightSource.intensity *= _levelBlueprint.DarknessLevel;
            }
        }

        private void AssignMasterSwitchers()
        {
            var currentRoot = _levelBlueprint.CurrentRoot;
            var spawnedRooms = _levelBlueprint.SpawnedRooms;
            if (currentRoot)
            {
                var mSwitchers = currentRoot.ElectricPanel.MasterSwitchers;
                _lightMasterSwitcher = mSwitchers[Random.Range(0, mSwitchers.Length)];
                foreach (var lightExtended in currentRoot.LightExtended)
                {
                    lightExtended.MasterSwitcher = _lightMasterSwitcher;
                    _allLights.Add(lightExtended.Light);
                }
                foreach (var spawnedRoom in spawnedRooms)
                {
                    foreach (var lightExtended in spawnedRoom.LightExtended)
                    {
                        lightExtended.MasterSwitcher = _lightMasterSwitcher;
                        _allLights.Add(lightExtended.Light);
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