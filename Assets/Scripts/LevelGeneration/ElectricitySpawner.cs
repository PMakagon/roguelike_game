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
        private MasterSwitcher _lightMasterSwitcher;
        public List<Room> SpawnedRooms { get; set; }
        public RootRoom CurrentRoot { get; set; }
        private List<Light> _allLights = new List<Light>();
        public bool enableElectricityGeneration;


        private void Update()
        {
            if (enableElectricityGeneration)
            {
                enableElectricityGeneration = false;
                AssignMasterSwitchers();
            }
        }

        private void AdjustLevelDarkness()
        {
            foreach (var lightsource in _allLights)
            {
            }
        }

        private void AssignMasterSwitchers()
        {
            if (CurrentRoot)
            {
                var mSwitchers = CurrentRoot.ElectricPanel.MasterSwitchers;
                _lightMasterSwitcher = mSwitchers[Random.Range(0, mSwitchers.Length)];
                foreach (var lightExtended in CurrentRoot.LightExtended)
                {
                    lightExtended.MasterSwitcher = _lightMasterSwitcher;
                }
                foreach (var spawnedRoom in SpawnedRooms)
                {
                    foreach (var lightExtended in spawnedRoom.LightExtended)
                    {
                        lightExtended.MasterSwitcher = _lightMasterSwitcher;
                        _allLights.Add(lightExtended.Light);
                    }
                }

            }
        }
    }
}