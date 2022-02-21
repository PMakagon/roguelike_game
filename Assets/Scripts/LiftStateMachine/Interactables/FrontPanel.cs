using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiftStateMachine
{
    public class FrontPanel : MonoBehaviour
    {
        [SerializeField] private GameObject button;
        [SerializeField] private LiftControllerData liftControllerData;
        // [SerializeField] private Animator buttonAnimation;
        
        [SerializeField] private int thisFloorNumber;

       

        public void OnButtonInteract()
        {
            liftControllerData.CurrentFloor = thisFloorNumber;
            liftControllerData.IsLiftCalled = true;
            // buttonAnimation.SetBool("IsPressed",liftControllerData.IsLiftCalled);
            button.GetComponent<Renderer>().material.color=Color.black;
        }

        
    }
}