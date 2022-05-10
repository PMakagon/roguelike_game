using UnityEngine;

namespace LevelGeneration
{
    public class Wall :MonoBehaviour

    { 
        [SerializeField] private MConnector joint;
        private bool isConnected;

        public MConnector Joint => joint;

        public bool IsConnected
        {
            get => isConnected;
            set => isConnected = value;
        }
    }
}