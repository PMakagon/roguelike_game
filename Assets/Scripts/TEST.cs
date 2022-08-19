using UnityEngine;

namespace LiftGame
{
    public class TEST : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("AWAKE");
        }

        private void OnEnable()
        {
            Debug.Log("ONENABLE");

        }

        private void Start()
        {
            Debug.Log("START");

        }

        private void FixedUpdate()
        {
            Debug.Log("FIXED UPDATE");

        }

        private void Update()
        {
            Debug.Log("UPDATE");

        }

        private void LateUpdate()
        {
            Debug.Log("LATEUPDATE");

        }
    }
}