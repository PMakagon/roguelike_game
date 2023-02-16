using System;
using System.Collections.Generic;
using LiftGame.PlayerCore;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LiftGame.Ui.RequiredItemsMenu
{
    public class RequiredItemsMenu : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private ItemRequirementView itemRequirementViewPrefab;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Button confirmBtn;
        [SerializeField] private Button closeBtn;
        [SerializeField] private HorizontalLayoutGroup itemsLayoutGroup;
        [SerializeField] private Texture2D menuCursorSprite;
        [SerializeField] private Image menuCursor;
        
        private List<ItemRequirementView> _spawnedRequirementViews = new List<ItemRequirementView>();
        private bool _isReady;
        private bool _isCursorActive;
        private PointerEventData _currentEventData;
        private Canvas _canvas;
        private RectTransform _canvasRect;
        private PlayerServiceProvider _serviceProvider;

        public Action OnConfirmPressed;
        public Action OnClose;
        public Action OnMouseEnter;
        public Action OnMouseExit;

        private const string CONFIRM = "CONFIRM";
        private const string FILL = "FILL";

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasRect = _canvas.transform as RectTransform;
            confirmBtn.onClick.AddListener(Confirm);
            closeBtn.onClick.AddListener(Close);
        }

        // private void Update()
        // {
        //     if (_currentEventData == null) return;
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, _currentEventData.position, _canvas.worldCamera,  out var newValue);
        //     menuCursor.rectTransform.localPosition = newValue;
        //     
        // }

        private void OnEnable()
        {
            UpdateViews();
        }

        private void OnDisable()
        {
            OnClose?.Invoke();
        }

        public void Initialize(List<BuildMenuItemRequirementData> items, string description)
        {
            descriptionText.text = description;
            foreach (var requirementData in items)
            {
                var newRequiredItem = Instantiate(itemRequirementViewPrefab, itemsLayoutGroup.transform);
                newRequiredItem.Render(requirementData);
                _spawnedRequirementViews.Add(newRequiredItem);
            }
        }

        public void UpdateViews()
        {
            if (_spawnedRequirementViews.IsEmpty()) return;
            foreach (var requirementView in _spawnedRequirementViews)
            {
                requirementView.UpdateCounter();
            }
        }

        private void Confirm()
        {
            OnConfirmPressed?.Invoke();
        }
        private void Close()
        {
            OnClose?.Invoke();
        }

        public void SetReady(bool state)
        {
            _isReady = state;
            buttonText.text = _isReady ? CONFIRM : FILL;
            if (_isReady)
            {
            }
        }

        public Texture2D MenuCursorSprite => menuCursorSprite;

        public bool IsReady => _isReady;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _currentEventData = eventData;
            // menuCursor.gameObject.SetActive(true);
            OnMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _currentEventData = null;
            // menuCursor.gameObject.SetActive(false);
            OnMouseExit?.Invoke();
        }
    }
}