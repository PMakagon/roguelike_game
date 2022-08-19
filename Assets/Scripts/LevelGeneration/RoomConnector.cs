using UnityEngine;

namespace LiftGame.LevelGeneration
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