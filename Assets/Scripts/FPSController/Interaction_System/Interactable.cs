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
    [SerializeField] private string tooltipMessage = "interact";
    // [SerializeField] private UnityEvent interacted;
    #endregion

    #region Properties

    public bool IsInteractable => isInteractable;
    public bool HoldInteract => holdInteract;
    public float HoldDuration => holdDuration;

    public string TooltipMessage => tooltipMessage;
    

    #endregion

    #region Methods

    public virtual void OnInteract(InventoryData inventoryData)
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
    // public virtual void OnInteract()
    // {
    //     // interacted?.Invoke();
    //     Debug.Log("INTERACTED: " + gameObject.name);
    // }
    //
    #endregion

    }
}  
