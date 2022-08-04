using System;
using UnityEngine;

namespace LiftStateMachine
{
    public class LiftInnerTrigger : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                liftControllerData.IsPlayerInside = true;
                // Debug.Log("PLAYER INSIDE");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                liftControllerData.IsPlayerInside = false;
                // Debug.Log("PLAYER OUTSIDE");
            }
        }
    }
}
