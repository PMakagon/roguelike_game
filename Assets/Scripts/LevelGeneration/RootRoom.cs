using UnityEngine;

namespace LevelGeneration
{
    public class RootRoom : MonoBehaviour 

    {
        [SerializeField] private Transform floorTransform;
        [SerializeField] private FConnector[] connectors;

        private bool isReady;

        public bool IsReady => isReady;

        private void Awake()
        {
            if (!floorTransform)
            {
                Debug.Log("NO FLOOR POSITION");
            }

        }

        public Transform FloorTransform
        {
            get => floorTransform;
            set => floorTransform = value;
        }

        public FConnector[] Connectors
        {
            get => connectors;
            set => connectors = value;
        }
    }
}