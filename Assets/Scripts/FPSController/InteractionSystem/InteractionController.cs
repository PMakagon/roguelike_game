using LiftGame.FPSController.ScriptableObjects;
using LiftGame.InventorySystem;
using LiftGame.PlayerCore;
using LiftGame.Ui;
using UnityEngine;
using Zenject;

namespace LiftGame.FPSController.InteractionSystem
{
    public class InteractionController : MonoBehaviour
    {
        [Space, Header("Data")] 
        [SerializeField] private InteractionInputData interactionInputData = null;
        [SerializeField] private InteractionData interactionData = null;
        [Space, Header("UI")] [SerializeField] private InteractionUIPanel uiPanel;

        [Space, Header("Ray Settings")]
        [SerializeField] private float rayDistance = 0f;

        [SerializeField] private float raySphereRadius = 0f;
        [SerializeField] private LayerMask interactableLayer = ~0;

        private Camera _cam;

        private bool _interacting;
        private float _holdTimer = 0f;

        private IPlayerData _playerData;

        [Inject]
        private void Construct(InteractionUIPanel interactionUIPanel,IPlayerData playerData)
        {
            _playerData = playerData;
            uiPanel = interactionUIPanel;
        }

        #region Built In Methods

        void Awake()
        {
            // _cam = FindObjectOfType<Camera>();
            _cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            CheckForInteractable();
            CheckForInteractableInput();
        }

        #endregion


        #region Custom methods

        private void CheckForInteractable()
        {
            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
            RaycastHit hitInfo;

            bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out hitInfo, rayDistance, interactableLayer);

            if (hitSomething)
            {
                Interactable interactable = hitInfo.transform.GetComponent<Interactable>();

                if (interactable != null)
                {
                    uiPanel.SetPanelActive(true);
                    if (interactionData.IsEmpty())
                    {
                        interactionData.Interactable = interactable;
                        uiPanel.SetTooltipActive(true); // включил текст
                        uiPanel.SetTooltip(interactable.TooltipMessage); 
                    }
                    else
                    {
                        if (!interactionData.IsSameInteractable(interactable))
                        {
                            interactionData.Interactable = interactable;
                            uiPanel.SetTooltipActive(true);// включил текст
                            uiPanel.SetTooltip(interactable.TooltipMessage);
                        }
                    }
                    return;
                }
            }
            //когда будешь юзать слои верни сюда else
            if (uiPanel != null)
            {
                uiPanel.ResetUI();
            }
            interactionData.ResetData();
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
        }

        private void CheckForInteractableInput()
        {
            if (interactionData.IsEmpty())
                return;

            if (interactionInputData.InteractedClicked)
            {
                _interacting = true;
                _holdTimer = 0f;
            }

            if (interactionInputData.InteractedReleased)
            {
                _interacting = false;
                _holdTimer = 0f;
                uiPanel.UpdateProgressBar(0f);
            }

            if (_interacting)
            {
                if (!interactionData.Interactable.IsInteractable)
                    return;

                if (interactionData.Interactable.HoldInteract)
                {
                    uiPanel.SetProgressBarActive(true);
                    _holdTimer += Time.deltaTime;

                    float heldPercent =  (_holdTimer / interactionData.Interactable.HoldDuration) * 10;
                    uiPanel.UpdateProgressBar(heldPercent);

                    if (heldPercent > uiPanel.ProgressBar.maxValue)
                    {
                        interactionData.Interact(_playerData);
                        uiPanel.SetProgressBarActive(false);
                        _interacting = false;
                    }
                }
                else
                {
                    interactionData.Interact(_playerData);
                    uiPanel.SetProgressBarActive(false);
                    _interacting = false;
                }
            }
        }

        #endregion
    }
}