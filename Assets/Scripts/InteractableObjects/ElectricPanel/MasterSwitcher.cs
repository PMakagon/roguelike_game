using System.Collections;
using System.Collections.Generic;
using FPSController;
using LightingSystem;
using UnityEngine;

public class MasterSwitcher : Interactable
{
    [SerializeField] private bool isSwitchedOn;
    [SerializeField] private Animation switchAnimation;
    
    

    public bool IsSwitchedOn
    {
        get => isSwitchedOn;
        set => isSwitchedOn = value;
    }

    public override void OnInteract()
    {
        // switchAnimation.Play();
        isSwitchedOn = !isSwitchedOn;
        base.OnInteract();
    }
}
