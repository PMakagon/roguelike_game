using System.Collections.Generic;
using System.Linq;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.InteractableObjects;
using LiftGame.ProxyEventHolders.Player;
using LiftGame.Ui.RequiredItemsMenu;
using UnityEngine;
using UnityEngine.Events;

namespace LiftGame.Ui
{
    public class BuildInteractable : Interactable
    {
        [SerializeField] private InteractableUI interactableUIPrefab;
        [SerializeField] private List<BuildMenuItemRequirementData> requirements;
        [SerializeField] private Transform menuHolder;
        [SerializeField] private string description;
        [SerializeField] private float hideDistance = 3f;
        [SerializeField] private UnityEvent onBuild;

        private RequiredItemsMenu.RequiredItemsMenu _spawnedMenu;
        private Interaction _toInspect = new Interaction("Inspect", 1, true);
        private Interaction _toBuild = new Interaction("Build", 5, false);

        public override void BindInteractions()
        {
            _toInspect.actionOnInteract = Inspect;
            _toBuild.actionOnInteract = Build;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toInspect);
            Interactions.Add(_toBuild);
            
        }

        private bool Inspect()
        {
            if (!_spawnedMenu)
            {
                _spawnedMenu = Instantiate(interactableUIPrefab, menuHolder.position, Quaternion.identity,
                    menuHolder).MenuCanvas;
                _spawnedMenu.Initialize(requirements, description);
            }

            EnableMenu();
            CheckRequirements();
            return true;
        }

        private void EnableMenu()
        {
            _spawnedMenu.gameObject.SetActive(true);
            _spawnedMenu.OnClose += DisableMenu;
            _spawnedMenu.OnConfirmPressed += OnConfirm;
            PlayerInventoryEventHolder.OnInventoryChange += CheckRequirements;
        }


        private void DisableMenu()
        {
            _spawnedMenu.gameObject.SetActive(false);
            _spawnedMenu.OnClose -= DisableMenu;
            _spawnedMenu.OnConfirmPressed -= OnConfirm;
            PlayerInventoryEventHolder.OnInventoryChange -= CheckRequirements;
        }

        private void CheckRequirements()
        {
            foreach (var requirement in requirements)
            {
                requirement.CurrentAmount =
                    CachedServiceProvider.InventoryService.CountItemByDefinition(requirement.RequiredItem);
                if (requirement.RequiredAmount <= requirement.CurrentAmount)
                {
                    requirement.IsFulfilled = true;
                }
            }
            _spawnedMenu.UpdateViews(); 
            _spawnedMenu.SetReady(requirements.All(requirement => requirement.IsFulfilled));
        }

        private void TryFill()
        {
            foreach (var requirement in requirements.Where(requirement => requirement.RemoveOnConfirm))
            {
                CachedServiceProvider.InventoryService.RemoveAllByDefinition(requirement.RequiredItem,
                    requirement.RequiredAmount);
            }
        }

        private void OnConfirm()
        {
            TryFill();
            CheckRequirements();
            if (!_spawnedMenu.IsReady)
            {
                return;
            }
            // Interactions.Add(_toBuild);
            _toBuild.IsEnabled = true;
            DisableMenu();
        }

        private bool Build()
        {
            CheckRequirements();
            if (_spawnedMenu.IsReady)
            {
                onBuild.AddListener((() => Debug.Log("BUILD")));
                onBuild.Invoke();
                Destroy(_spawnedMenu.gameObject);
                gameObject.SetActive(false);
                return true;
            }
            return false;
        }

        private void RotateMenu()
        {
            menuHolder.LookAt(CachedServiceProvider.CameraController.PlayerCamera.transform);
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(menuHolder.position, CachedServiceProvider.FPSController.transform.position) >
                hideDistance)
            {
                DisableMenu();
            }
        }

        private void FixedUpdate()
        {
            if (_spawnedMenu && _spawnedMenu.gameObject.activeSelf)
            {
                RotateMenu();
                CheckDistance();
            }
        }

        private void OnDestroy()
        {
            // _spawnedMenu.OnClose -= DisableMenu;
            // _spawnedMenu.OnConfirmPressed -= OnConfirm;
            Destroy(_spawnedMenu);
        }

        public override void PostInteract()
        {
        }
    }
}