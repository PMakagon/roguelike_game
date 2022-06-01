using System;
using System.Collections.Generic;
using InteractableObjects;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class DoorSpawner : MonoBehaviour
    {
        [SerializeField] private Entry[] dormPrefabs;
        [SerializeField] private Entry[] electricDoorPrefabs;
        private List<Room> spawnedRooms;
        public LevelBlueprint LevelBlueprint { get; set; }
        private bool _isDoorSpawnerEnabled;


        private void Update()
        {
            if (_isDoorSpawnerEnabled)
            {
                _isDoorSpawnerEnabled = false;
                spawnedRooms = LevelBlueprint.SpawnedRooms;
                GenerateFirstDoors();
            }
        }

        [ContextMenu("GENERATE DOORS")]
        private void GenerateFirstDoors()
        {
            foreach (var room in spawnedRooms)
            {
                if (room.DoorMarks.Length != 0)
                {
                    var doorMarks = room.DoorMarks;
                    foreach (var doorMark in doorMarks)
                    {
                        var randomDoorIndex = Random.Range(0, dormPrefabs.Length);
                        var newDoor = Instantiate(dormPrefabs[randomDoorIndex]);
                        Align(newDoor.transform, newDoor.DoorConnector.transform, doorMark.transform);
                        newDoor.transform.SetParent(doorMark.transform, true);
                    }
                }
            }
        }

        private void Align(Transform door, Transform mConnector, Transform fConnector)
        {
            if (!door || !mConnector || !fConnector) return;
            door.rotation = fConnector.rotation *
                            Quaternion.Inverse(Quaternion.Inverse(door.rotation) * mConnector.rotation);
            door.position = fConnector.position + (door.position - mConnector.position);
        }

        public List<Room> SpawnedRooms
        {
            get => spawnedRooms;
            set => spawnedRooms = value;
        }

        public bool EnableDoorSpawner
        {
            get => _isDoorSpawnerEnabled;
            set => _isDoorSpawnerEnabled = value;
        }
    }
}