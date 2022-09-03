using UnityEngine;

namespace LiftGame.Ui
{
    public class LoadingIcon : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private bool _isActive;

        private void Update()
        {
            if(_isActive == false)
                return;
            transform.Rotate(Vector3.forward * speed);
        }

        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;
    }
}