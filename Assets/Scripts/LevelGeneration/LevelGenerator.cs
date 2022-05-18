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
        [SerializeField] private Wall wall;

        private Dictionary<Room.RoomType, Room[]> roomsMap = new Dictionary<Room.RoomType, Room[]>();
        private List<Room> spawnedRooms = new List<Room>();
        private bool isGenerating = false;
        private Vector3 startPosition;
        private RootRoom currentRoot;
        private bool rootReady;

        private int destroyedRooms;
        private int spawnAttempts;
        
        [Header("CONTROLS")]
        public bool enableGeneration;
        public bool destroyLevel;
        public bool reportRealTime = true;
        public bool showReport;
        private void Awake()
        {
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
                if (currentRoot)
                {
                    Destroy(currentRoot.gameObject);
                    spawnedRooms.Clear();
                }
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
            Debug.Log("<b>Attempts done - </b>" + spawnAttempts);
            Debug.Log("<color=red><b>-------------------</b></color>");
        }

        private IEnumerator SpawnRoom(FConnector connector)
        {
            isGenerating = true;
            spawnAttempts++;
            var randomTypeIndex = Random.Range(0, connector.AllowedRoomTypes.Length);//случайный разрешенный тип комнаты
            var randomRoomType = connector.AllowedRoomTypes[randomTypeIndex];//определяем тип комнаты для коннектора
            var roomsOfType = roomsMap[randomRoomType];// берем массив с выбранными комнатами
            var randomRoomIndex = Random.Range(0, roomsOfType.Length);// случайная комната выбранного типа
            var newRoom = Instantiate(roomsOfType[randomRoomIndex]);// спавним случайную комнату из массива
            newRoom.Align(newRoom.transform, newRoom.Entrance.transform, connector.transform);// выпрямляем комнату
            yield return null;

            if (!newRoom.IsConnected || newRoom.IsInvalid) // валидация новой комнаты
            {
                Destroy(newRoom.gameObject);
                if (connector.IterationsBeforeWall<=0)
                {
                    var newWall = Instantiate(wall);
                    Align(newWall.transform, newWall.Joint.transform, connector.transform);
                    if (newWall.Joint.transform.position==connector.transform.position)
                    {
                        newWall.IsConnected= true;
                        newWall.transform.SetParent(connector.transform, true);
                        yield break;
                    }
                }
                else
                {
                    connector.IterationsBeforeWall--;
                }
                StartCoroutine(SpawnRoom(connector));
                isGenerating = false;
                destroyedRooms++;
                yield break;
            }

            yield return null;
            if (newRoom.IsReady)
            {
                newRoom.transform.SetParent(connector.transform, true);
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
            currentRoot = null;
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

        public RootRoom CurrentRoot => currentRoot;
    }
}