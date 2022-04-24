using UnityEngine;

namespace LevelGeneration
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] private Room myRoom;
        [SerializeField] private Collider myTrigger;
        private bool isTriggered;

        public bool IsTriggered => isTriggered;

        private void Start()
        {
            gameObject.tag = "RoomTrigger";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RoomTrigger"))
            {
                isTriggered = true;
                if (myRoom)
                {
                    myRoom.IsInvalid = true;
                }
                // Debug.Log("ROOM IS INVALID",myRoom);
            }
        }

        void Update()
        {
        
        }
    }
}
