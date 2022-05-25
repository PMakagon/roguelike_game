using LightingSystem;
using UnityEngine;

namespace LevelGeneration
{
    public class RootRoom : MonoBehaviour

    {
        [SerializeField] private Transform floorTransform;
        [SerializeField] private FConnector[] connectors;
        [SerializeField] private LightExtended[] lightExtended;
        [SerializeField] private ElectricPanel electricPanel;

        // [SerializeField] private int difficulty;
        private bool isReady;

        public bool IsReady => isReady;

        public LightExtended[] LightExtended
        {
            get => lightExtended;
            set => lightExtended = value;
        }

        public ElectricPanel ElectricPanel
        {
            get => electricPanel;
            set => electricPanel = value;
        }

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