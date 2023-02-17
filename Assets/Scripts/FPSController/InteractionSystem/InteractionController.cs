using LiftGame.FPSController.InteractionSystem.InteractionUI;
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;
using LiftGame.InteractableObjects;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.FPSController.InteractionSystem
{
    public class InteractionController : MonoBehaviour, IPauseable
    {
        [Space, Header("UI")] 
        [SerializeField] private InteractionMenu interactionPanel;

        [Space, Header("Ray Settings")] 
        [SerializeField] private float rayDistance = 0f;
        [SerializeField] private float raySphereRadius = 0f;
        [SerializeField] private LayerMask interactableLayer = ~0;

        private InteractionInputData _interactionInputData = null;
        private EquipmentInputData _equipmentInputData = null;
        private IPauseHandler _pauseHandler;
        private PlayerServiceProvider _serviceProvider;
        private Transform _camTransform;

        private IInteractable _currentInteractable = null;
        private IInteractable _cachedInteractable = null;

        private bool _interacting;
        private float _holdTimer = 0f;
        private bool _isPaused;

        //MonoBehaviour injection
        [Inject]
        private void Construct(InteractionMenu interactionMenu, InputDataProvider inputDataProvider,IPauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
            _interactionInputData = inputDataProvider.InteractionInputData;
            _equipmentInputData = inputDataProvider.EquipmentInputData;
            interactionPanel = interactionMenu;
        }

        #region Unity Callbacks

        void Awake()
        {
            _serviceProvider = GetComponent<PlayerServiceProvider>();
            _camTransform = GetComponentInChildren<Camera>().transform;
            _pauseHandler.Register(this);
            _serviceProvider.InventoryService.OnInventoryOpen += interactionPanel.HidePanel;
        }

        private void OnDestroy()
        {
            _serviceProvider.InventoryService.OnInventoryOpen -= interactionPanel.HidePanel;
        }

        void Update()
        {
            if (_serviceProvider.InventoryService.IsInventoryOpen || _isPaused) return;
            CheckForInteractable();
            CheckForInteractableInput();
        }

        #endregion

        #region Custom methods

        private bool IsNewInteractable(IInteractable newInteractable) => _currentInteractable != newInteractable;
        private bool IsPreviousInteractable(IInteractable newInteractable) => _cachedInteractable == newInteractable;
        private bool IsEmpty() => _currentInteractable == null;
        private void ClearInteractable()
        {
            _currentInteractable?.PostInteract();
            _currentInteractable = null;
        }

        private void SetCachedInteractable(IInteractable newInteractable)
        {
            _cachedInteractable = newInteractable;
        }

        private void CheckForInteractable()
        {
            Ray ray = new Ray(_camTransform.position, _camTransform.forward);
            RaycastHit hitInfo;

            bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out hitInfo, rayDistance, interactableLayer);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);

            if (hitSomething)
            {
                IInteractable interactable = hitInfo.transform.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactionPanel.ShowPanel();
                    
                    if (IsEmpty() || IsNewInteractable(interactable))
                    {
                        interactable.PreInteract(_serviceProvider);
                        _currentInteractable = interactable;
                        interactionPanel.SetCrosshair(interactable);
                        interactionPanel.ShowTooltip(interactable.TooltipMessage);
                        if (!interactable.IsInteractable)
                        {
                            interactionPanel.ResetMenu();
                            return;
                        }
                        interactionPanel.ShowOptionMenu();
                        if (!IsPreviousInteractable(interactable))
                        {
                            SetCachedInteractable(interactable);
                            interactionPanel.CreateMenuOptions(_currentInteractable.Interactions);
                        }
                        interactionPanel.ShowOptionMenu();
                        interactionPanel.UpdateOptionsState(_serviceProvider);
                        interactionPanel.SetNewSelection();
                    }

                    if (!_interacting && !IsNewInteractable(interactable))
                    {
                        interactionPanel.UpdateOptionsState(_serviceProvider);
                    }

                    return;
                }
            }

            interactionPanel.HidePanel();
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

                    float heldPercent =
                        (_holdTimer / interactionPanel.SelectedOption.RepresentedInteraction.HoldDuration) * 10;
                    interactionPanel.SelectedOption.UpdateProgressBar(heldPercent);

                    if (heldPercent > interactionPanel.SelectedOption.ProgressBar.maxValue)
                    {
                        if (interactionPanel.SelectedOption.RepresentedInteraction.IsEquipmentUseNeeded &&
                            !_equipmentInputData.UsingClicked) return;
                        interactionPanel.SelectedOption.ResetProgressBar();
                        interactionPanel.SelectedOption.ConfirmSelection();
                        _currentInteractable.OnInteract(interactionPanel.SelectedOption.RepresentedInteraction);
                        interactionPanel.UpdateOptionsState(_serviceProvider);
                        ClearInteractable();
                        _interacting = false;
                    }
                }
                else
                {
                    interactionPanel.SelectedOption.ConfirmSelection();
                    _currentInteractable.OnInteract(interactionPanel.SelectedOption.RepresentedInteraction);
                    interactionPanel.UpdateOptionsState(_serviceProvider);
                    ClearInteractable();
                    _interacting = false;
                }
            }
        }

        #endregion

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}