using UnityEngine;

namespace LevelGeneration
{
    public class FConnector : RoomConnector
    {
        enum ConnectorDirection
        {
            Forward,
            Back,
            Left,
            Right
        }
    
        [SerializeField] private Room.RoomType[] allowedRoomTypes;
        [Tooltip("FORWARD = +Z,BACK = -Z,RIGHT = +X,LEFT = -X")] 
        [SerializeField] private ConnectorDirection spawnDirection;
        private Vector3 _direction;
        
        public Vector3 Direction => _direction;
        public Room.RoomType[] AllowedRoomTypes => allowedRoomTypes;

        private void Awake()
        {
            // SetDirection();
            SetZero();
        }

        private void SetZero()
        {
            switch (spawnDirection)
            {
                case ConnectorDirection.Forward:
                    transform.Rotate(0,0,0);
                    break;
                case ConnectorDirection.Back:
                    transform.Rotate(0,0,0);
                    break;
                case ConnectorDirection.Left:
                    transform.Rotate(0,-0,0);
                    break;
                case ConnectorDirection.Right:
                    transform.Rotate(0,0,0);
                    break;
            }
        }
        private void SetDirection()
        {
            switch (spawnDirection)
            {
                case ConnectorDirection.Forward:
                    transform.Rotate(0,-90,0);
                    break;
                case ConnectorDirection.Back:
                    transform.Rotate(0,90,0);
                    break;
                case ConnectorDirection.Left:
                    transform.Rotate(0,-180,0);
                    break;
                case ConnectorDirection.Right:
                    transform.Rotate(0,0,0);
                    break;
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var c = new Color(0.8f, 0, 0, 0.4f);
            UnityEditor.Handles.color = c;
            var position = transform.position;
        
            _direction = spawnDirection switch
            {
                ConnectorDirection.Forward => position + Vector3.forward.normalized,
                ConnectorDirection.Back => position + Vector3.back.normalized,
                ConnectorDirection.Left => position + Vector3.left.normalized,
                ConnectorDirection.Right => position + Vector3.right.normalized,
                _ => _direction
            };
        
            UnityEditor.Handles.DrawLine(position,_direction);
        }
#endif
    }
}