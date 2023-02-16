using System.Collections.Generic;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerCore;
using LiftGame.Ui.RequiredItemsMenu;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class InteractableUI :MonoBehaviour, IInteractable
    {
        [SerializeField] private RequiredItemsMenu menuCanvas;
        private bool _isCursorOn;
        private Vector2 _cachedLookSensitivity;
        public bool IsInteractable { get; } = false;
        public string TooltipMessage { get; set; } = "";
        public PlayerServiceProvider CachedServiceProvider { get; private set; }
        public List<Interaction> Interactions { get; } = new List<Interaction>();
        
        private void ResetCache()
        {
            CachedServiceProvider = null;
        }

        public void PreInteract(PlayerServiceProvider serviceProvider)
        {
            CachedServiceProvider = serviceProvider;
            _cachedLookSensitivity = CachedServiceProvider.CameraController.Sensitivity;
            EnableMouseInteraction();
        }

        public void OnInteract(Interaction interaction)
        {
           
        }

        public void PostInteract()
        {
            DisableMouseInteraction();
            ResetCache();
        }

        private void Update()
        {
            if (_isCursorOn)
            {
                
            }
        }

        private void EnableMouseInteraction()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            // Cursor.SetCursor(menuCanvas.MenuCursorSprite,Vector2.zero, CursorMode.Auto);
            CachedServiceProvider.CameraController.Sensitivity = new Vector2(10, 10);
            menuCanvas.OnClose += DisableMouseInteraction;
            menuCanvas.OnMouseExit += DisableMouseInteraction;
            menuCanvas.OnMouseEnter += EnableMouseInteraction;
            _isCursorOn = true;
            Debug.Log("ENABLE");

        } 
        
        private void DisableMouseInteraction()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            // Cursor.SetCursor(null,Vector2.zero, CursorMode.Auto);
            CachedServiceProvider.CameraController.Sensitivity = _cachedLookSensitivity;
            menuCanvas.OnClose -= DisableMouseInteraction;
            menuCanvas.OnMouseExit -= DisableMouseInteraction;
            menuCanvas.OnMouseEnter -= EnableMouseInteraction;
            _isCursorOn = false;
            Debug.Log("DISABLE");
        }
        
        public RequiredItemsMenu MenuCanvas => menuCanvas;
    }
}