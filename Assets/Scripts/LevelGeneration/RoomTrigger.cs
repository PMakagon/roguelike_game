using UnityEngine;

namespace LiftGame.LevelGeneration
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] private Room myRoom;
        [SerializeField] private Collider myTrigger;
        private bool _isTriggered;

        public bool IsTriggered => _isTriggered;

        // private void Awake()
        // {
        //     myRoom = GetComponentInParent<Room>();
        // }

        private void Start()
        {
            gameObject.tag = "RoomTrigger";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RoomTrigger"))
            {
                _isTriggered = true;
                if (myRoom)
                {
                    myRoom.IsInvalid = true;
                }
                // Debug.Log("ROOM IS INVALID",myRoom);
            }
        }
        
    }
}
