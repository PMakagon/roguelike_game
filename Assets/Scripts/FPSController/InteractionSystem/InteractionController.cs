using LiftGame.FPSController.InteractionSystem.InteractionUI;
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;
using LiftGame.Inventory;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.FPSController.InteractionSystem
{
    public class InteractionController : MonoBehaviour , IPauseable
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
        private IPlayerData _playerData;
        private IPlayerInventoryService _playerInventoryService;
        private Camera _cam;

        private IInteractable _currentInteractable = null;
        private IInteractable _cachedInteractable = null;

        private bool _interacting;
        private float _holdTimer = 0f;
        private bool _isPaused;

        //MonoBehaviour injection
        [Inject]
        private void Construct(InteractionMenu interactionMenu, IPlayerData playerData,
            InputDataProvider inputDataProvider, IPlayerInventoryService playerInventoryService,IPauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
            _interactionInputData = inputDataProvider.InteractionInputData;
            _equipmentInputData = inputDataProvider.EquipmentInputData;
            _playerData = playerData;
            interactionPanel = interactionMenu;
            _playerInventoryService = playerInventoryService;
        }

        #region Unity Callbacks

        void Awake()
        {
            _cam = GetComponentInChildren<Camera>();
            _pauseHandler.Register(this);
            _playerInventoryService.OnInventoryOpen += interactionPanel.HidePanel;
        }

        private void OnDestroy()
        {
            _playerInventoryService.OnInventoryOpen -= interactionPanel.HidePanel;
        }

        void Update()
        {
            if (_playerInventoryService.IsInventoryOpen || _isPaused) return;
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
                    interactionPanel.ShowPanel();
                    if (IsEmpty() && IsPreviousInteractable(interactable))
                    {
                        interactable.PreInteract(_playerData);
                        _currentInteractable = interactable;
                        interactionPanel.SetCrosshair(interactable); //хуйня идея
                        interactionPanel.ShowTooltip(interactable.TooltipMessage);
                        interactionPanel.ShowOptionMenu();
                        interactionPanel.UpdateOptionsState(_playerData);
                        interactionPanel.SetNewSelection();
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
                        interactionPanel.SetNewSelection();
                    }

                    if (!_interacting && !IsNewInteractable(interactable))
                    {
                        interactionPanel.UpdateOptionsState(_playerData);
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

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}