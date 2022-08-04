using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        private Transform _parentPanel;
        
        private IItem _itemInSlot;

        [SerializeField] private TextMeshProUGUI _name;
        
        [SerializeField] private Image _mainIcon;
        
        [SerializeField] private Image _selectionIcon;
        
        [SerializeField] private Image _defaultIcon;

        [SerializeField] private TextMeshProUGUI _amountText;

        [SerializeField] private int _amount;
        
        
        private void Start()
        {
            // _name = GetComponentInChildren<TextMeshProUGUI>();
            // _amountText = GetComponentInChildren<TextMeshProUGUI>();
            // _mainIcon = GetComponentInChildren<Image>();
            _selectionIcon.enabled = false;
            if (_itemInSlot==null)
            {
                _mainIcon.enabled = false;
            }
        }

        public void RenderSlot()
        {
            if (_itemInSlot!=null)
            {
                _name.text = _itemInSlot.Name;
                _mainIcon.enabled = true;
                _mainIcon.sprite = _itemInSlot.UIIcon;
            }
            else
            {
                _name.text = string.Empty;
                _mainIcon.enabled = false;
                _mainIcon.sprite = null;
            }

            // _amountText.text = _amount.ToString();
        }

        public void EnableSelection(bool state)
        {
            _selectionIcon.enabled = state;
        }

        public void SetSelectionColor(Color color)
        {
            _selectionIcon.color = color;
        }


        public void SetOnDragState()
        {
            _defaultIcon.raycastTarget = false;
            _defaultIcon.enabled = false;
            _selectionIcon.enabled = false;
            _name.enabled = false;
            _amountText.enabled = false;
        }
        
        public void UpdateAmount(int amountToAdd)
        {
            _amount += amountToAdd;
        }

        public IItem ItemInSlot
        {
            get => _itemInSlot;
            set => _itemInSlot = value;
        }

        public TextMeshProUGUI Name
        {
            get => _name;
            set => _name = value;
        }

        public Image Icon
        {
            get => _mainIcon;
            set => _mainIcon = value;
        }
        

        public TextMeshProUGUI AmountText
        {
            get => _amountText;
            set => _amountText = value;
        }

        public Transform ParentPanel
        {
            get => _parentPanel;
            set => _parentPanel = value;
        }
    }
}
