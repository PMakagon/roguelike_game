using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Items/Item")]
public class Key : ScriptableObject, IItem
{
    [SerializeField] private string _name;

    // [SerializeField] private string _itemType;

    [SerializeField] private Sprite _uiicon;

    [SerializeField] private string _description;

    public string Name { get; }

    // public string ItemType { get; }
    public Sprite UIIcon { get; }
    public string Description { get; }
    public bool IsKey { get; }
}