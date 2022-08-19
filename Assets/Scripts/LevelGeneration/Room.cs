using LiftGame.LightingSystem;
using UnityEngine;

namespace LiftGame.LevelGeneration
{
    public class Room : MonoBehaviour
    {
        public enum RoomType
        {
            Hall,
            Corner,
            Cross,
            Big,
            Medium,
            Small,
            // Special
        }
    
        [SerializeField] private RoomType roomType;
        [SerializeField] private MConnector entrance;
        [SerializeField] private FConnector[] exits;
        [SerializeField] private bool hasExit;
        [SerializeField] private bool hasDoorMarks;
        // [SerializeField] private RoomTrigger roomTrigger;//удалить если не нужно
        [SerializeField] private MConnector[] doorMarks;
        // [SerializeField] private LightSpawnPoint[] lightSpawnPoints;
        [SerializeField] private SlaveSwitcher[] slaveSwitchers;
        [SerializeField] private LightExtended[] lightExtended;

        public SlaveSwitcher[] SlaveSwitchers
        {
            get => slaveSwitchers;
            set => slaveSwitchers = value;
        }

        public LightExtended[] LightExtended
        {
            get => lightExtended;
            set => lightExtended = value;
        }

        private bool isConnected;
        private bool isReady;
        public bool IsInvalid { get; set; }
        public bool IsConnected => isConnected;
        public bool IsReady => isReady;
        public MConnector Entrance => entrance;
        public MConnector[] DoorMarks => doorMarks;
        public FConnector[] Exits => exits;
        public bool HasExit => hasExit;
        
        private void Awake()
        {
            if (exits.Length!=0)
            {
                hasExit = true;
            } 
        }

        private void Update()
        {
            if (isConnected && !IsInvalid)
            { 
                isReady = true;
            }
        }
        
        public void Align(Transform room, Transform mConnector, Transform fConnector)
        {
            if (room && mConnector && fConnector)
            {
                room.rotation = fConnector.rotation * Quaternion.Inverse(Quaternion.Inverse(room.rotation) * mConnector.rotation);
                room.position = fConnector.position + (room.position - mConnector.position);
            }

            if (mConnector.position==fConnector.position)
            {
                isConnected = true;
            }
            else
            {
                Debug.Log("ROOM NOT CONNECTED", this);
            }
        }
    }
}