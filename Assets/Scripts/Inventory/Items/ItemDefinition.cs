using LiftGame.Inventory.Core;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    public abstract class ItemDefinition : ScriptableObject, IInventoryItem
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite = null;
        [SerializeField] private GameObject worldItemPrefab = null;
        [SerializeField] private InventoryShape _shape = null;
        [SerializeField] private ItemType _type = ItemType.Default;
        [SerializeField] private bool _canDrop = true;
        [SerializeField, HideInInspector] private Vector2Int _position = Vector2Int.zero;

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name => this.name;

        public string Description => _description;

        /// <summary>
        /// The type of the item
        /// </summary>
        public virtual ItemType ItemType
        {
            get => ItemType.Default;
            set => _type = value;
        }

        /// <inheritdoc />
        public Sprite sprite => _sprite;

        /// <inheritdoc />
        public int width => _shape.width;

        /// <inheritdoc />
        public int height => _shape.height;

        /// <inheritdoc />
        public Vector2Int position
        {
            get => _position;
            set => _position = value;
        }

        /// <inheritdoc />
        public bool IsPartOfShape(Vector2Int localPosition)
        {
            return _shape.IsPartOfShape(localPosition);
        }

        /// <inheritdoc />
        public bool canDrop => _canDrop;

        /// <summary>
        /// Creates a copy if this scriptable object
        /// </summary>
        public IInventoryItem CreateInstance()
        {
            var clone = ScriptableObject.Instantiate(this);
            clone.name = clone.name.Substring(0, clone.name.Length - 7); // Remove (Clone) from name
            return clone;
        }

        public void SpawnWorldItem(Vector3 positionToSpawn,Transform parent)
        {
            if (parent)
            {
                Instantiate(worldItemPrefab,positionToSpawn,Quaternion.identity,parent);
            }else
            {
                Instantiate(worldItemPrefab,positionToSpawn,Quaternion.identity);
            }
        }
    }
}