using LiftGame.LevelGeneration;
using UnityEditor;
using UnityEngine;

namespace LiftGame.Editor
{
    public enum ConnectionType
    {
        Room,
        Connector
    }
    public class RoomConnectionTool : EditorWindow
    {
        private ConnectionType _connectionType = ConnectionType.Room;
        private Room _roomToConnect;
        private Room _roomConnectTo;
        private FConnector _connector;
        private Entry _doorToSpawn;
        private bool _isConnectionTypeSelected = false;
        private int _selectedExit = 0;
        
        [MenuItem("Tools/RoomConnectionTool")]
        private static void ShowWindow()
        {
            GetWindow<RoomConnectionTool>();
        }

        private void OnGUI()
        {
            _connectionType =
                (ConnectionType)EditorGUILayout.EnumPopup("Connection Type", _connectionType);
            
            _roomToConnect = EditorGUILayout.ObjectField("Room To Connect", _roomToConnect, typeof(Room), true) as Room;
            
            if (_connectionType==ConnectionType.Room)
            {
                _roomConnectTo = EditorGUILayout.ObjectField("Room Connect To", _roomConnectTo, typeof(Room), true) as Room;
                if (_roomConnectTo!=null && _roomConnectTo.HasExit)
                {
                    _selectedExit = _roomConnectTo.Exits.Length>1 ? EditorGUILayout.IntSlider("Exit number", _selectedExit, 1, _roomConnectTo.Exits.Length - 1) : 0;
                }
            }
            else
            {
                _connector = EditorGUILayout.ObjectField("Connector", _connector, typeof(FConnector), true) as FConnector;
            }

            _doorToSpawn = EditorGUILayout.ObjectField("Door To Spawn", _doorToSpawn, typeof(Entry), true) as Entry;

            if (!GUILayout.Button("CONNECT")) return;
            if (_connectionType == ConnectionType.Connector)
            {
                ConnectRoomToConnector();
            }
            else
            {
                ConnectRoomToRoom(_selectedExit);
            }
            SpawnDoor();
        }

        public void ConnectRoomToConnector()
        {
            
            if (!_roomToConnect || !_connector) return;
            var roomTransform = _roomToConnect.transform;
            var connectorTransform = _connector.transform;
            roomTransform.rotation = connectorTransform.rotation * Quaternion.Inverse(Quaternion.Inverse(roomTransform.rotation) * _roomToConnect.Entrance.transform.rotation);
            roomTransform.position = connectorTransform.position + (roomTransform.position - _roomToConnect.Entrance.transform.position);
           
        }

        private void SpawnDoor()
        {
            if (!_doorToSpawn) return;
            _doorToSpawn.GetComponents();
            var doorTransform = _doorToSpawn.transform;
            var connectorTransform = _connector.transform;
            doorTransform.rotation = connectorTransform.rotation * Quaternion.Inverse(Quaternion.Inverse( doorTransform.rotation) * _doorToSpawn.DoorConnector.transform.rotation);
            doorTransform.position =  connectorTransform.position + (doorTransform.position - _doorToSpawn.DoorConnector.transform.position);
        }
        
        public void ConnectRoomToRoom(int selectedExit)
        {
            
            if (!_roomToConnect || !_roomConnectTo) return;
            
            var roomTransform = _roomToConnect.transform;
            var connectorTransform = _roomConnectTo.Exits[selectedExit].transform;
            roomTransform.rotation = connectorTransform.rotation * Quaternion.Inverse(Quaternion.Inverse(roomTransform.rotation) * _roomToConnect.Entrance.transform.rotation);
            roomTransform.position = connectorTransform.position + (roomTransform.position - _roomToConnect.Entrance.transform.position);
            if (_doorToSpawn)
            {
                _doorToSpawn.GetComponents();
                var doorTransform = _doorToSpawn.transform;
                doorTransform.rotation = connectorTransform.rotation * Quaternion.Inverse(Quaternion.Inverse( doorTransform.rotation) * _doorToSpawn.DoorConnector.transform.rotation);
                doorTransform.position =  connectorTransform.position + (doorTransform.position - _doorToSpawn.DoorConnector.transform.position);
            }
        }
    }
}