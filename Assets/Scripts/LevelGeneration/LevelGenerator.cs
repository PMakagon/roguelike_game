using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        private enum LevelType
        {
            Dorm
        }

        [SerializeField] private Transform startPoint;
        [SerializeField] private LevelType levelType;
        [SerializeField] private RootRoom rootRoom;

        [Header("PREFABS")]
        [SerializeField] private Room[] halls;
        [SerializeField] private Room[] big;
        [SerializeField] private Room[] medium;
        [SerializeField] private Room[] small;
        [SerializeField] private Room[] cross;

        private Dictionary<Room.RoomType, Room[]> roomsMap = new Dictionary<Room.RoomType, Room[]>();
        private List<Room> spawnedRooms = new List<Room>();
        private bool isGenerating = false;
        private Vector3 startPosition;
        private RootRoom currentRoot;
        private bool rootReady;

        private int destroyedRooms;
        private int attempts;
        
        [Header("CONTROLS")]
        public bool enableGeneration;
        public bool destroyLevel;
        public bool reportRealTime = true;
        public bool showReport;
        private void Awake()
        {
            // startPosition = startPoint.position;
            roomsMap.Add(Room.RoomType.Hall, halls);
            roomsMap.Add(Room.RoomType.Big, big);
            roomsMap.Add(Room.RoomType.Medium, medium);
            roomsMap.Add(Room.RoomType.Small, small);
            roomsMap.Add(Room.RoomType.Cross, cross);
        }

        private void Update()
        {
            if (enableGeneration)
            {
                enableGeneration = false;
                if (!currentRoot)
                {
                    SpawnRoot();
                }
                GenerateFirstRooms();
            }

            if (showReport)
            {
                showReport = false;
                if (rootReady && !isGenerating)
                {
                    WriteReport();
                }
            }
            
            if (destroyLevel)
            {
                destroyLevel = false;
                Destroy(currentRoot.gameObject);
                spawnedRooms.Clear();
            }
        }

        private void SpawnRoot()
        {
            if (levelType == LevelType.Dorm)
            {
                currentRoot = Instantiate(rootRoom, startPoint.position, Quaternion.identity);
                Align(currentRoot.transform,currentRoot.FloorTransform,startPoint);
            }
            Debug.Log("Root Spawned");
        }

        [ContextMenu("STOP GENERATION")]
        private void StopGeneration()
        {
            StopAllCoroutines();
        }

        private void WriteReport()
        {
            var invalidCount=0;
            var notReadyCount=0;
            foreach (var spawnedRoom in spawnedRooms)
            {
                if (!spawnedRoom.IsReady)
                {
                    // if (reportRealTime) Debug.Log("<b>ROOM NOT READY</b>", spawnedRoom);
                    notReadyCount++;

                }
                if (spawnedRoom.IsInvalid)
                {
                    // if (reportRealTime) Debug.Log("<b>ROOM IS INVALID</b>", spawnedRoom);
                    invalidCount++;
                }
            }
            Debug.Log("<color=red><b>-------------------</b></color>");
            Debug.Log("<b>Rooms spawned - </b>" + spawnedRooms.Count);
            Debug.Log("<b>Rooms NOT ready - </b>" + notReadyCount);
            Debug.Log("<b>Rooms INVALID - </b>" + invalidCount);
            Debug.Log("<b>Rooms destroyed - </b>" + destroyedRooms);
            Debug.Log("<b>Attempts done - </b>" + attempts);
            Debug.Log("<color=red><b>-------------------</b></color>");
        }

        private IEnumerator SpawnRoom(FConnector connector)
        {
            isGenerating = true;
            attempts++;
            var randomTypeIndex = Random.Range(0, connector.AllowedRoomTypes.Length);
            var randomRoomType = connector.AllowedRoomTypes[randomTypeIndex];
            var roomsOfType = roomsMap[randomRoomType];
            var randomRoomIndex = Random.Range(0, roomsOfType.Length);
            var newRoom = Instantiate(roomsOfType[randomRoomIndex]);
            newRoom.Align(newRoom.transform, newRoom.Entrance.transform, connector.transform);
            newRoom.transform.SetParent(connector.transform, true);
            yield return null;
            
            if (!newRoom.IsConnected || newRoom.IsInvalid)
            {
                Destroy(newRoom.gameObject);
                StartCoroutine(SpawnRoom(connector));
                isGenerating = false;
                destroyedRooms++;
                yield break;
            }

            yield return null;
            if (newRoom.IsReady)
            {
                connector.IsConnected = true;///мне не нравится что я это снаружи делаю
                if (newRoom.HasExit)
                {
                    SpawnNextRooms(newRoom);
                }
                else
                {
                    spawnedRooms.Add(newRoom);
                }
            }
            isGenerating = false;
        }

        private void GenerateFirstRooms()
        {
            foreach (var rootConnector in currentRoot.Connectors)
            {
                if (!rootConnector.IsConnected)
                {
                    StartCoroutine(SpawnRoom(rootConnector));
                }
            }
            rootReady = true;
        }

        private void SpawnNextRooms(Room spawnedRoom)
        {
            foreach (var exit in spawnedRoom.Exits)
            {
                StartCoroutine(SpawnRoom(exit));
            }
        }
        
        private void Align(Transform room, Transform mConnector, Transform fConnector)
        {
            if (!room || !mConnector || !fConnector) return;
            room.rotation = fConnector.rotation * Quaternion.Inverse(Quaternion.Inverse(room.rotation) * mConnector.rotation);
            room.position = fConnector.position + (room.position - mConnector.position);

        }
        
        public Transform StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }
    }
}