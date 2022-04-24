
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Items/Item")]
public class Item : ScriptableObject, IItem
{
    [SerializeField] private string _name;
    // [SerializeField] private string _itemType;
    [SerializeField] private Sprite _uiicon;
    [SerializeField] private string _description;
    [SerializeField] public bool isKey;
    public string Name => _name;


    // public string ItemType => _itemType;

    public Sprite UIIcon => _uiicon;

    public string Description => _description;
    
    public bool IsKey => isKey;
}
