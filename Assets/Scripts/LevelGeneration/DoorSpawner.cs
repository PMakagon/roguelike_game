using System.Collections.Generic;
using InteractableObjects;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

namespace LevelGeneration
{
    public class DoorSpawner : MonoBehaviour
    {
        [SerializeField] private List<Room> spawnedRooms;
        [SerializeField] private LevelGenerator _levelGenerator;
        
        //эти данные должны предоставлять извне
        private int amountOfBossRooms;
        private int amountOfSpecialRooms;

        [SerializeField] private InteractableDoor[] doorPrefabs;
        
        [ContextMenu("GENERATE DOORS")]
        private void GenerateFirstDoors()
        {
            spawnedRooms = _levelGenerator.SpawnedRooms;
            foreach (var room in spawnedRooms)
            {
                if (room.DoorMarks.Length!=0)
                {
                    var doorMarks = room.DoorMarks;
                    foreach (var doorMark in doorMarks)
                    {
                        var randomDoorIndex = Random.Range(0, doorPrefabs.Length);
                        var newDoor = Instantiate(doorPrefabs[randomDoorIndex]);
                        Align(newDoor.transform,newDoor.DoorConnector.transform,doorMark.transform);
                    }
                }
            }
        }
        private void Align(Transform door, Transform mConnector, Transform fConnector)
        {
            if (!door || !mConnector || !fConnector) return;
            door.rotation = fConnector.rotation * Quaternion.Inverse(Quaternion.Inverse(door.rotation) * mConnector.rotation);
            door.position = fConnector.position + (door.position - mConnector.position);
        }   
    }
}