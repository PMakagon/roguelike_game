using InventorySystem;
using NaughtyAttributes;
using UnityEngine;

namespace FPSController.Interaction_System
{
    public abstract class Interactable : MonoBehaviour , IInteractable
    {

    #region Variables
    [Space][Header("INTERACTABLE SETTINGS")]
    [SerializeField] private bool isInteractable = true;
    [ShowIf("isInteractable")][SerializeField] private bool holdInteract = true;
    [ShowIf("holdInteract")] [SerializeField] private float holdDuration = 1f;
    [SerializeField] private string tooltipMessage = "Interact";
    
    #endregion

    #region Properties

    public bool IsInteractable => isInteractable;
    public bool HoldInteract => holdInteract;
    public float HoldDuration => holdDuration;

    public string TooltipMessage
    {
        get => tooltipMessage;
        set => tooltipMessage = value;
    }
    
    // public string TooltipMessage => tooltipMessage;
    

    #endregion

    #region Methods

    public virtual void OnInteract(InventoryData inventoryData)
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
    #endregion

    }
}  
