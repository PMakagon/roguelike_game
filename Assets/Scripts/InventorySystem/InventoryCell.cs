using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;

    public void Render(Item item)
    {
        _name.text = item.Name;
        _icon.sprite = item.UIIcon;
    }
}
