using LiftGame.InteractableObjects;
using UnityEngine;

namespace LiftGame.LevelGeneration
{
    public class Entry : MonoBehaviour
    { 
        private InteractableDoor _door;
        private MConnector _doorConnector;

        private void Awake()
        {
            _doorConnector = GetComponentInChildren<MConnector>();
            _door = GetComponentInChildren<InteractableDoor>();
        }

        public InteractableDoor Door
        {
            get => _door;
            set => _door = value;
        }

        public MConnector DoorConnector => _doorConnector;
    }
}