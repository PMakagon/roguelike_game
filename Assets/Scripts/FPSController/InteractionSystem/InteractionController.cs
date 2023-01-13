using LiftGame.FPSController.InteractionSystem.InteractionMenu;
using LiftGame.GameCore.Input.Data;
using LiftGame.PlayerCore;
using LiftGame.Ui.HUD;
using UnityEngine;
using Zenject;

namespace LiftGame.FPSController.InteractionSystem
{
    public class InteractionController : MonoBehaviour
    {
         [Space, Header("UI")] 
        [SerializeField] private InteractionMenu.InteractionMenu interactionPanel;

        [Space, Header("Ray Settings")] 
        [SerializeField] private float rayDistance = 0f;
        [SerializeField] private float raySphereRadius = 0f;
        [SerializeField] private LayerMask interactableLayer = ~0;

        private InteractionInputData _interactionInputData = null;
        private EquipmentInputData _equipmentInputData = null;
        private IPlayerData _playerData;
        private Camera _cam;

        private IInteractable _currentInteractable = null;
        private IInteractable _cachedInteractable = null;

        private bool _interacting;
        private float _holdTimer = 0f;

        //MonoBehaviour injection
        [Inject]
        private void Construct(InteractionMenu.InteractionMenu interactionMenu, IPlayerData playerData,InputDataProvider inputDataProvider)
        {
            _interactionInputData = inputDataProvider.InteractionInputData;
            _playerData = playerData;
            interactionPanel = interactionMenu;
        }

        #region Built In Methods

        void Awake()
        {
            _cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            CheckForInteractable();
            CheckForInteractableInput();
        }

        #endregion

        #region Custom methods

        private bool IsNewInteractable(IInteractable newInteractable) => _currentInteractable != newInteractable;
        private bool IsPreviousInteractable(IInteractable newInteractable) => _cachedInteractable == newInteractable;
        private bool IsEmpty() => _currentInteractable == null;
        private void ClearInteractable() => _currentInteractable = null;

        private void SetCachedInteractable(IInteractable newInteractable)
        {
            _cachedInteractable?.PostInteract();
            _cachedInteractable = newInteractable;
        }

        private void CheckForInteractable()
        {
            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
            RaycastHit hitInfo;

            bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out hitInfo, rayDistance, interactableLayer);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
            
            if (hitSomething)
            {
                IInteractable interactable = hitInfo.transform.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactionPanel.SetPanelActive(true);
                    if (IsEmpty() && IsPreviousInteractable(interactable))
                    {
                        interactable.PreInteract(_playerData);
                        _currentInteractable = interactable;
                        interactionPanel.SetCrosshair(interactable); //хуйня идея
                        interactionPanel.ShowTooltip(interactable.TooltipMessage);
                        interactionPanel.ShowOptionMenu();
                        interactionPanel.UpdateOptionsState(_playerData);
                        return;
                    }


                    if (IsEmpty() || IsNewInteractable(interactable))
                    {
                        interactable.PreInteract(_playerData);
                        _currentInteractable = interactable;
                        SetCachedInteractable(interactable);
                        interactionPanel.SetCrosshair(interactable); //хуйня идея
                        interactionPanel.ShowTooltip(interactable.TooltipMessage);
                        interactionPanel.CreateMenuOptions(_currentInteractable.Interactions);
                        interactionPanel.ShowOptionMenu();
                        interactionPanel.UpdateOptionsState(_playerData);
                    }

                    if (!_interacting && !IsNewInteractable(interactable))
                    {
                        interactionPanel.UpdateOptionsState(_playerData);
                    }

                    return;
                }
            }

            interactionPanel.SetPanelActive(false);
            interactionPanel.ResetMenu();
            ClearInteractable();
        }

        private void CheckForInteractableInput()
        {
            if (IsEmpty())
                return;

            if (_interactionInputData.InteractedClicked)
            {
                _interacting = true;
                _holdTimer = 0f;
                
            }

            if (_interactionInputData.InteractedReleased)
            {
                _interacting = false;
                _holdTimer = 0f;
                if (_currentInteractable.IsInteractable) interactionPanel.SelectedOption.ResetProgressBar();
            }

            if (_interacting)
            {
                if (!_currentInteractable.IsInteractable)
                    return;

                if (interactionPanel.SelectedOption.RepresentedInteraction.IsHoldInteract)
                {
                    interactionPanel.SelectedOption.SetProgressBarActive();
                    _holdTimer += Time.deltaTime;

                    float heldPercent = (_holdTimer / interactionPanel.SelectedOption.RepresentedInteraction.HoldDuration) * 10;
                    interactionPanel.SelectedOption.UpdateProgressBar(heldPercent);

                    if (heldPercent > interactionPanel.SelectedOption.ProgressBar.maxValue)
                    {
                        if (interactionPanel.SelectedOption.RepresentedInteraction.IsEquipmentUseNeeded && !_equipmentInputData.UsingClicked) return;
                        interactionPanel.SelectedOption.ResetProgressBar();
                        interactionPanel.SelectedOption.ConfirmSelection();
                        _currentInteractable.OnInteract(interactionPanel.SelectedOption.RepresentedInteraction);
                        interactionPanel.UpdateOptionsState(_playerData);
                        ClearInteractable();
                        _interacting = false;
                    }
                }
                else
                {
                    interactionPanel.SelectedOption.ConfirmSelection();
                    _currentInteractable.OnInteract(interactionPanel.SelectedOption.RepresentedInteraction);
                    interactionPanel.UpdateOptionsState(_playerData);
                    ClearInteractable();
                    _interacting = false;
                }
            }
        }

        #endregion
    }
}