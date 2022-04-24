using UnityEngine;

namespace LevelGeneration
{
    public abstract class RoomConnector : MonoBehaviour
    {
        public bool IsConnected { get; set; }

        public virtual bool GetStatus()
        {
            return IsConnected;
        }

    }
}