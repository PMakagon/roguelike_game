using LiftGame.Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui.RequiredItemsMenu
{
    public class ItemRequirementView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;
        [SerializeField] private Image equipmentIcon;
        private BuildMenuItemRequirementData _requirementData;
        private Image _itemImage;
        private ItemDefinition _item;
        private Rect _rect;

        private void Awake()
        {
            _itemImage = GetComponent<Image>();
            _rect = GetComponent<RectTransform>().rect;
        }

        public void Render(BuildMenuItemRequirementData requirementData)
        {
            _requirementData = requirementData;
            _item = requirementData.RequiredItem;
            _rect.width = _item.width;
            _rect.height = _item.height;
            _itemImage.sprite = _item.sprite;
            counter.text = requirementData.CounterText;
            equipmentIcon.gameObject.SetActive(!requirementData.RemoveOnConfirm);
        }

        public void UpdateCounter()
        {
            counter.text = _requirementData.CounterText;
            counter.color = _requirementData.IsFulfilled? Color.green : Color.white;
        }
    }
}