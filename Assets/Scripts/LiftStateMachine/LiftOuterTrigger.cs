using UnityEngine;

namespace LiftGame.LiftStateMachine
{
    public class LiftOuterTrigger : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                liftControllerData.IsPlayerLeft = false;
                // Debug.Log("PLAYER CLOSE TO LIFT");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                liftControllerData.IsPlayerLeft = true;
                // Debug.Log("PLAYER LEFT");
            }
        }
    }
}