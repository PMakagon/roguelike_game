using System;
using System.Collections.Generic;
using LiftGame.GameCore.Input.Data;
using LiftGame.PlayerCore;
using ModestTree;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem.InteractionMenu
{
    public class InteractionMenu : MonoBehaviour //rename InteractionMenu
    {
        [SerializeField] private InteractionTooltip tooltip;
        [SerializeField] private CrosshairSpriteChanger crosshair;
        [SerializeField] private RectTransform canvasTransform;

        [SerializeField] private InteractionMenuOption menuOptionPrefab;
        [SerializeField] private RectTransform optionMenuTransform;
        [SerializeField] private RectTransform layoutOptionPanel;
        private List<Interaction> _interactions;
        private InteractionMenuOption[] _spawnedOptionsArr;
        private int _selectionIndex = 0;
        private InteractionMenuOption _selectedOption;
        private float _scrollPosition = 1f;

        public InteractionMenuOption SelectedOption => _selectedOption;

        private bool _isOpen = false;

        public void SetPanelActive(bool state)
        {
            canvasTransform.gameObject.SetActive(state);
        }

        public void SetCrosshair(IInteractable interactable)
        {
            if (interactable.IsInteractable)
            {
                crosshair.ChangeCrosshair(CrosshairType.Hand);
            }
            else
            {
                crosshair.ChangeCrosshair(CrosshairType.Dot);
            }
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
            if (Math.Abs(scrollInput) < 0.2f) return;
            _scrollPosition = Math.Clamp(_scrollPosition + scrollInput * 0.5f, 0, _spawnedOptionsArr.Length - 1);
            Debug.Log(_scrollPosition);
            int newIndex = _selectionIndex;
            if (_scrollPosition is >= 0 and < 1)
            {
                newIndex = 0;
            }

            if (_scrollPosition is >= 1 and < 2)
            {
                newIndex = 1;
            }

            if (_scrollPosition >= 2)
            {
                newIndex = 2;
            }

            // Debug.Log("scrollInput=" + scrollInput + " _scrollPosition=" + _scrollPosition + " _selectionIndex=" +
            //           _selectionIndex + " newIndex" + newIndex);
            // if (newIndex==_selectionIndex) return;
            _spawnedOptionsArr[_selectionIndex].UnSelect();
            _selectionIndex = newIndex;
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
            _isOpen = true;
        }

        public void UpdateOptionsState(IPlayerData playerData)
        {
            foreach (var option in _spawnedOptionsArr)
            {
                option.Update();
                option.RepresentedInteraction.CheckIsExecutable(playerData);
                option.gameObject.SetActive(option.RepresentedInteraction.IsExecutable);
            }
            ChooseNewSelection();
        }

        private void ChooseNewSelection()
        {
            var counter = 0;
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