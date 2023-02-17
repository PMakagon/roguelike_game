using LiftGame.Inventory.Core;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    public abstract class ItemDefinition : ScriptableObject, IInventoryItem
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite = null;
        [ShowAssetPreview(128, 128)][SerializeField] private GameObject worldItemPrefab = null;
        [SerializeField] private InventoryShape _shape = null;
        [SerializeField] private ItemType _type = ItemType.Default;
        [SerializeField] private bool _canDrop = true;
        [SerializeField, HideInInspector] private Vector2Int _position = Vector2Int.zero;

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name 
        {
            get => name;
            protected set => name = value;
        }

        public string Description 
        {
            get => _description;
            protected set => _description = value;
        }

        /// <summary>
        /// The type of the item
        /// </summary>
        public virtual ItemType ItemType
        {
            get => ItemType.Default;
            set => _type = value;
        }

        /// <inheritdoc />
        public Sprite sprite
        {
            get => _sprite;
            protected set => _sprite = value;
        }

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

        public void SpawnWorldItem(Vector3 positionToSpawn)
        {
            if (worldItemPrefab != null) Instantiate(worldItemPrefab,positionToSpawn,Quaternion.identity);
        }
    }
}