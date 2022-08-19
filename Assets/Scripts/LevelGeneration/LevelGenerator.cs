using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LiftGame.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        //убрать это отсюда
        public enum LevelType
        {
            Dorm
        }

        [SerializeField] private Transform startPoint;
        [SerializeField] private LevelType levelType;
        [SerializeField] private RootRoom rootRoom;

        [Header("PREFABS")] [SerializeField] private Room[] halls;
        [SerializeField] private Room[] big;
        [SerializeField] private Room[] medium;
        [SerializeField] private Room[] small;
        [SerializeField] private Room[] cross;
        [SerializeField] private Room[] corners;
        [SerializeField] private Wall wall;

        private Dictionary<Room.RoomType, Room[]> _roomsMap = new Dictionary<Room.RoomType, Room[]>();
        private bool _rootReady;
        private bool _isGenerating = false;

        [Header("GENERATOR OUTPUT")] private List<Room> _spawnedRooms = new List<Room>();
        private RootRoom _currentRoot;

        [Header("STATS FOR REPORT")] private int _destroyedRooms;
        private int _spawnAttempts;

        [Header("CONTROLS")] public bool enableGeneration;
        public bool destroyLevel;
        public bool reportRealTime = true;
        public bool showReport;

        [Header("GENERATION PARAM")] [SerializeField]
        private int _levelSize;
        private int _hallsAllowed;

        private int _hallsCounter;
        private int _stairsCounter;
        private int _bossRoomsCounter;

        public LevelBlueprint LevelBlueprint { get; set; }

        private int _spawnCounter;
        private bool _levelReady;

        private void Awake()
        {
            _roomsMap.Add(Room.RoomType.Hall, halls);
            _roomsMap.Add(Room.RoomType.Big, big);
            _roomsMap.Add(Room.RoomType.Medium, medium);
            _roomsMap.Add(Room.RoomType.Small, small);
            _roomsMap.Add(Room.RoomType.Cross, cross);
            _roomsMap.Add(Room.RoomType.Corner, corners);
        }

        public void ResetLevelGenerator()
        {
            _currentRoot = null;
            _spawnedRooms = new List<Room>();
            _destroyedRooms = 0;
            _spawnAttempts = 0;
            _levelReady = false;
            _rootReady = false;
        }

        private void Update()
        {
            if (enableGeneration)
            {
                enableGeneration = false;
                if (!_currentRoot)
                {
                    GetLevelSize(LevelBlueprint.LevelSize);
                    SpawnRoot();
                    GenerateFirstRooms();
                }
            }

            if (showReport)
            {
                showReport = false;
                if (_rootReady && !_isGenerating)
                {
                    WriteReport();
                }
            }

            if (destroyLevel)
            {
                destroyLevel = false;
                if (_currentRoot)
                {
                    Destroy(_currentRoot.gameObject);
                    _spawnedRooms.Clear();
                    _levelReady = false;
                    _rootReady = false;
                }
            }

            if (_rootReady && _spawnCounter == 0)
            {
                _levelReady = true;
            }
        }

        [ContextMenu("STOP GENERATION")]
        private void StopGeneration()
        {
            StopAllCoroutines();
        }

        private void WriteReport()
        {
            var invalidCount = 0;
            var notReadyCount = 0;
            foreach (var spawnedRoom in _spawnedRooms)
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
            Debug.Log("<b>Rooms spawned - </b>" + _spawnedRooms.Count);
            Debug.Log("<b>Rooms NOT ready - </b>" + notReadyCount);
            Debug.Log("<b>Rooms INVALID - </b>" + invalidCount);
            Debug.Log("<b>Rooms destroyed - </b>" + _destroyedRooms);
            Debug.Log("<b>Attempts done - </b>" + _spawnAttempts);
            Debug.Log("<color=red><b>-------------------</b></color>");
        }

        private void GetLevelSize(int levelSize)
        {
            switch (levelSize)
            {
                case 1:
                    _hallsAllowed = 2;
                    return;
                case 2:
                    _hallsAllowed = 4;
                    return;
                case 3:
                    _hallsAllowed = 8;
                    return;
                case 4:
                    _hallsAllowed = 10;
                    return;
                default:
                    _hallsAllowed = 2;
                    return;
            }
        }

        private bool ApproveRoomType(Room.RoomType roomType)
        {
            if (roomType == Room.RoomType.Hall)
            {
                if (_hallsCounter + 1 > _hallsAllowed)
                {
                    return false;
                }

                _hallsCounter++;
                return true;
            }

            return true;
        }

        private IEnumerator SpawnRoom(FConnector connector)
        {
            _isGenerating = true;
            _spawnAttempts++;
            _spawnCounter++;
            Room.RoomType randomRoomType;
            do
            {
                var randomAllowedTypeIndex =
                    Random.Range(0, connector.AllowedRoomTypes.Length); //случайный разрешенный тип комнаты
                randomRoomType =
                    connector.AllowedRoomTypes[randomAllowedTypeIndex]; //определяем тип комнаты для коннектора
            } while (!ApproveRoomType(randomRoomType));

            var roomsOfChosenType = _roomsMap[randomRoomType]; // берем массив с выбранными комнатами
            var randomRoomIndex = Random.Range(0, roomsOfChosenType.Length); // случайная комната выбранного типа
            var newRoom = Instantiate(roomsOfChosenType[randomRoomIndex]); // спавним случайную комнату из массива
            newRoom.Align(newRoom.transform, newRoom.Entrance.transform, connector.transform); // выпрямляем комнату
            yield return null;
            yield return null;
            if (!newRoom.IsConnected || newRoom.IsInvalid) // валидация новой комнаты
            {
                Destroy(newRoom.gameObject);
                _destroyedRooms++;
                if (connector.IterationsBeforeWall <= 0)
                {
                    var newWall = Instantiate(wall);
                    Align(newWall.transform, newWall.Joint.transform, connector.transform);
                    if (newWall.Joint.transform.position == connector.transform.position)
                    {
                        newWall.IsConnected = true;
                        newWall.transform.SetParent(connector.transform, true);
                        _isGenerating = false;
                        _spawnCounter--;
                        yield break;
                    }
                }
                else
                {
                    connector.IterationsBeforeWall--;
                }
                StartCoroutine(SpawnRoom(connector));
                _isGenerating = false;
                _spawnCounter--;
                yield break;
            }
            yield return null;
            if (newRoom.IsReady)
            {
                newRoom.transform.SetParent(connector.transform, true);
                connector.IsConnected = true; ///мне не нравится что я это снаружи делаю
                if (newRoom.HasExit)
                {
                    SpawnNextRooms(newRoom);
                }

                _spawnedRooms.Add(newRoom);
            }

            _spawnCounter--;
            _isGenerating = false;
        }

        private void SpawnRoot()
        {
            if (levelType == LevelType.Dorm)
            {
                _currentRoot = Instantiate(rootRoom, startPoint.position, Quaternion.identity);
                Align(_currentRoot.transform, _currentRoot.FloorTransform, startPoint);
            }

            if (reportRealTime)
            {
                Debug.Log("Root Spawned");
            }
        }

        private void GenerateFirstRooms()
        {
            foreach (var rootConnector in _currentRoot.Connectors)
            {
                if (!rootConnector.IsConnected)
                {
                    StartCoroutine(SpawnRoom(rootConnector));
                }
            }

            _rootReady = true;
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
            room.rotation = fConnector.rotation *
                            Quaternion.Inverse(Quaternion.Inverse(room.rotation) * mConnector.rotation);
            room.position = fConnector.position + (room.position - mConnector.position);
        }

        public Transform StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }

        public bool LevelReady => _levelReady;

        public bool RootReady => _rootReady;

        public int SpawnCounter => _spawnCounter;
        public RootRoom CurrentRoot => _currentRoot;
        public List<Room> SpawnedRooms => _spawnedRooms;
    }
}