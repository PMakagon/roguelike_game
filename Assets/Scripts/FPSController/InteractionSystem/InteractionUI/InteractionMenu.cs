using System;
using System.Collections.Generic;
using LiftGame.GameCore.Input.Data;
using LiftGame.PlayerCore;
using ModestTree;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem.InteractionUI
{
    public class InteractionMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasTransform;
        [SerializeField] private InteractionTooltip tooltip;
        [SerializeField] private CrosshairSpriteChanger crosshair;

        [SerializeField] private InteractionMenuOption menuOptionPrefab;
        [SerializeField] private RectTransform optionMenuTransform;
        [SerializeField] private RectTransform layoutOptionPanel;
        private List<Interaction> _interactions;
        private InteractionMenuOption[] _spawnedOptionsArr;
        private int _selectionIndex = 0;
        private InteractionMenuOption _selectedOption;

        public InteractionMenuOption SelectedOption => _selectedOption;

        private bool _isOpen = false;

        public void ShowPanel()
        {
            canvasTransform.gameObject.SetActive(true);
        }
        public void HidePanel()
        {
            canvasTransform.gameObject.SetActive(false);
        }

        public void SetCrosshair(IInteractable interactable)
        {
            crosshair.ChangeCrosshair(interactable.IsInteractable ? CrosshairType.Hand : CrosshairType.Dot);
        }

        private void OnDisable()
        {
            UIInputData.OnScrolling -= ScrollSelection;
        }

        private void SetSelectedOption(int selectionIndex)
        {
            if (_selectedOption!=null)_selectedOption.UnSelect();
            _selectedOption = _spawnedOptionsArr[selectionIndex];
            _selectionIndex = selectionIndex;
            _selectedOption.Select();
        }

        private void ScrollSelection(float scrollInput)
        {
            if (!_isOpen) return;
            if (Math.Abs(scrollInput) < 0.1f) return;
            var direction  = Math.Sign(scrollInput);
            int pointIndex = Math.Clamp(_selectionIndex + direction, 0, _spawnedOptionsArr.Length - 1);
            if (pointIndex==_selectionIndex || _spawnedOptionsArr[pointIndex].IsHidden) return;
            _spawnedOptionsArr[_selectionIndex].UnSelect();
            _selectionIndex = pointIndex;
            SetSelectedOption(_selectionIndex);
        }

        public void CreateMenuOptions(List<Interaction> iteractionsToDraw)
        {
            ClearOptionMenu();
            _interactions = iteractionsToDraw;
            _spawnedOptionsArr = new InteractionMenuOption[iteractionsToDraw.Count];
            var menuIndex = 0;
            foreach (var interaction in iteractionsToDraw)
            {
                var newOption = Instantiate(menuOptionPrefab, layoutOptionPanel);
                _spawnedOptionsArr[menuIndex] = newOption;
                newOption.Setup(interaction);
                newOption.gameObject.SetActive(newOption.RepresentedInteraction.IsExecutable);
                menuIndex++;
            }
        }

        public void UpdateOptionsState(IPlayerData playerData)
        {
            foreach (var option in _spawnedOptionsArr)
            {
                option.UpdateOption();
                option.RepresentedInteraction.CheckIsExecutable(playerData);
                option.gameObject.SetActive(option.RepresentedInteraction.IsExecutable);
            }
        }

        public void SetNewSelection()
        {
            // var counter = 0;
            foreach (var option in _spawnedOptionsArr)
            {
                if (option.gameObject.activeInHierarchy)
                {
                    SetSelectedOption(_spawnedOptionsArr.IndexOf(option));
                    return;
                }
            }
            // switch (counter)
            // {
            //     case 0:
            //         return;
            //     case 1:
            //         SetSelectedOption(0);
            //         break;
            //     case 2:
            //         SetSelectedOption(0);
            //         break;
            //     case 3:
            //         SetSelectedOption(1);
            //         break;
            //     case 4:
            //         SetSelectedOption(1);
            //         break;
            //     case 5:
            //         SetSelectedOption(2);
            //         break;
            // }
        }

        private void ClearOptionMenu()
        {
            if (_spawnedOptionsArr == null) return;
            foreach (var spawnedOption in _spawnedOptionsArr)
            {
                Destroy(spawnedOption.gameObject);
            }

            _spawnedOptionsArr = null;
            _interactions = null;
            _selectionIndex = 0;
        }

        public void ShowOptionMenu()
        {
            optionMenuTransform.gameObject.SetActive(true);
            _isOpen = true;
            UIInputData.OnScrolling += ScrollSelection;
        }

        public void HideOptionMenu()
        {
            optionMenuTransform.gameObject.SetActive(false);
            _isOpen = false;
            UIInputData.OnScrolling -= ScrollSelection;
        }


        public void HideTooltip()
        {
            tooltip.gameObject.SetActive(false);
        }

        public void ShowTooltip(string tooltipText)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.SetTooltip(tooltipText);
        }

        public void ResetMenu()
        {
            HideTooltip();
            HideOptionMenu();
        }
    }
}