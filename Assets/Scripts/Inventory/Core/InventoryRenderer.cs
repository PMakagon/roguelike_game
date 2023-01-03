using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Inventory.Core
{
    /// <summary>
    /// Renders a given inventory
    /// /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class InventoryRenderer : MonoBehaviour
    {
        [SerializeField, Tooltip("The size of the cells building up the inventory")]
        private Vector2Int _cellSize = new Vector2Int(32, 32);

        [SerializeField, Tooltip("The sprite to use for empty cells")]
        private Sprite _cellSpriteEmpty = null;

        [SerializeField, Tooltip("The sprite to use for selected cells")]
        private Sprite _cellSpriteSelected = null;
        
        [SerializeField, Tooltip("The sprite to use for hovered cells")]
        private Sprite _cellSpriteHovered = null;

        [SerializeField, Tooltip("The sprite to use for blocked cells")]
        private Sprite _cellSpriteBlocked = null;

        internal IRepositoryManager Repository;
        InventoryRenderMode _renderMode;
        private bool _haveListeners;
        private Pool<Image> _imagePool;
        private Image[] _grids;
        private Dictionary<IInventoryItem, Image> _items = new Dictionary<IInventoryItem, Image>();

        /*
         * Setup
         */
        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            // Create the image container
            var imageContainer = new GameObject("Image Pool").AddComponent<RectTransform>();
            imageContainer.transform.SetParent(transform);
            imageContainer.transform.localPosition = Vector3.zero;
            imageContainer.transform.localScale = Vector3.one;

            // Create pool of images
            _imagePool = new Pool<Image>(
                delegate
                {
                    var image = new GameObject("Image").AddComponent<Image>();
                    image.transform.SetParent(imageContainer);
                    image.transform.localScale = Vector3.one;
                    return image;
                });
        }

        /// <summary>
        /// Set what inventory to use when rendering
        /// </summary>
        public void SetInventory(IRepositoryManager repositoryManager, InventoryRenderMode renderMode)
        {
            OnDisable();
            Repository = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager)); 
            _renderMode = renderMode;
            OnEnable();
        }

        /// <summary>
        /// Returns the RectTransform for this renderer
        /// </summary>
        public RectTransform rectTransform { get; private set; }

        /// <summary>
        /// Returns the size of this inventory's cells
        /// </summary>
        public Vector2 cellSize => _cellSize;

        /* 
        Invoked when the inventory inventoryRenderer is enabled
        */
        void OnEnable()
        {
            if (Repository != null && !_haveListeners)
            {
                if (_cellSpriteEmpty == null) { throw new NullReferenceException("Sprite for empty cell is null"); }
                if (_cellSpriteSelected == null) { throw new NullReferenceException("Sprite for selected cells is null."); }
                if (_cellSpriteBlocked == null) { throw new NullReferenceException("Sprite for blocked cells is null."); }

                Repository.onRebuilt += ReRenderAllItems;
                Repository.onItemAdded += HandleItemAdded;
                Repository.onItemRemoved += HandleItemRemoved;
                Repository.onItemDropped += HandleItemRemoved;
                Repository.onResized += HandleResized;
                _haveListeners = true;

                // Render inventory
                ReRenderGrid();
                ReRenderAllItems();
            }
        }

        /* 
        Invoked when the inventory inventoryRenderer is disabled
        */
        void OnDisable()
        {
            if (Repository != null && _haveListeners)
            {
                Repository.onRebuilt -= ReRenderAllItems;
                Repository.onItemAdded -= HandleItemAdded;
                Repository.onItemRemoved -= HandleItemRemoved;
                Repository.onItemDropped -= HandleItemRemoved;
                Repository.onResized -= HandleResized;
                _haveListeners = false;
            }
        }

        /*
        Clears and renders the grid. This must be done whenever the size of the inventory changes
        */
        private void ReRenderGrid()
        {
            // Clear the grid
            if (_grids != null)
            {
                for (var i = 0; i < _grids.Length; i++)
                {
                    _grids[i].gameObject.SetActive(false);
                    RecycleImage(_grids[i]);
                    _grids[i].transform.SetSiblingIndex(i);
                }
            }
            _grids = null;

            // Render new grid
            var containerSize = new Vector2(cellSize.x * Repository.width, cellSize.y * Repository.height);
            Image grid;
            switch (_renderMode)
            {
                case InventoryRenderMode.Single:
                    grid = CreateImage(_cellSpriteEmpty, true);
                    grid.rectTransform.SetAsFirstSibling();
                    grid.type = Image.Type.Sliced;
                    grid.rectTransform.localPosition = Vector3.zero;
                    grid.rectTransform.sizeDelta = containerSize;
                    _grids = new[] { grid };
                    break;
                default:
                    // Spawn grid images
                    var topLeft = new Vector3(-containerSize.x / 2, -containerSize.y / 2, 0); // Calculate topleft corner
                    var halfCellSize = new Vector3(cellSize.x / 2, cellSize.y / 2, 0); // Calulcate cells half-size
                    _grids = new Image[Repository.width * Repository.height];
                    var c = 0;
                    for (int y = 0; y < Repository.height; y++)
                    {
                        for (int x = 0; x < Repository.width; x++)
                        {
                            grid = CreateImage(_cellSpriteEmpty, true);
                            grid.gameObject.name = "Grid " + c;
                            grid.rectTransform.SetAsFirstSibling();
                            grid.type = Image.Type.Sliced;
                            grid.rectTransform.localPosition = topLeft + new Vector3(cellSize.x * ((Repository.width - 1) - x), cellSize.y * y, 0) + halfCellSize;
                            grid.rectTransform.sizeDelta = cellSize;
                            _grids[c] = grid;
                            c++;
                        }
                    }
                    break;
            }

            // Set the size of the main RectTransform
            // This is useful as it allowes custom graphical elements
            // suchs as a border to mimic the size of the inventory.
            rectTransform.sizeDelta = containerSize;
        }

        /*
        Clears and renders all items
        */
        private void ReRenderAllItems()
        {
            // Clear all items
            foreach (var image in _items.Values)
            {
                image.gameObject.SetActive(false);
                RecycleImage(image);
            }
            _items.Clear();

            // Add all items
            foreach (var item in Repository.allItems)
            {
                if (item!=null)
                {
                    HandleItemAdded(item);
                }
            }
        }

        /*
        Handler for when inventory.OnItemAdded is invoked
        */
        private void HandleItemAdded(IInventoryItem item)
        {
            var img = CreateImage(item.sprite, false);

            if (_renderMode == InventoryRenderMode.Single)
            {
                img.rectTransform.localPosition = rectTransform.rect.center;
            }
            else
            {
                img.rectTransform.localPosition = GetItemOffset(item);
            }

            _items.Add(item, img);
        }

        /*
        Handler for when inventory.OnItemRemoved is invoked
        */
        private void HandleItemRemoved(IInventoryItem item)
        {
            if (_items.ContainsKey(item))
            {
                var image = _items[item];
                image.gameObject.SetActive(false);
                RecycleImage(image);
                _items.Remove(item);
            }
        }

        /*
        Handler for when inventory.OnResized is invoked
        */
        private void HandleResized()
        {
            ReRenderGrid();
            ReRenderAllItems();
        }

        /*
         * Create an image with given sprite and settings
         */
        private Image CreateImage(Sprite sprite, bool raycastTarget)
        {
            var img = _imagePool.Take();
            img.gameObject.SetActive(true);
            img.sprite = sprite;
            img.rectTransform.sizeDelta = new Vector2(img.sprite.rect.width, img.sprite.rect.height);
            img.transform.SetAsLastSibling();
            img.type = Image.Type.Simple;
            img.raycastTarget = raycastTarget;
            return img;
        }

        /*
         * Recycles given image 
         */
        private void RecycleImage(Image image)
        {
            image.gameObject.name = "Image";
            image.gameObject.SetActive(false);
            _imagePool.Recycle(image);
        }

        /// <summary>
        /// Selects a given item in the inventory
        /// </summary>
        /// <param name="item">Item to select</param>
        /// <param name="blocked">Should the selection be rendered as blocked</param>
        /// <param name="color">The color of the selection</param>
        public void SelectItem(IInventoryItem item, bool blocked, Color color)
        {
            if (item == null) { return; }
            ClearSelection();

            switch (_renderMode)
            {
                case InventoryRenderMode.Single:
                    _grids[0].sprite = blocked ? _cellSpriteBlocked : _cellSpriteSelected;
                    _grids[0].color = color;
                    break;
                default:
                    for (var x = 0; x < item.width; x++)
                    {
                        for (var y = 0; y < item.height; y++)
                        {
                            if (item.IsPartOfShape(new Vector2Int(x, y)))
                            {
                                var p = item.position + new Vector2Int(x, y);
                                if (p.x >= 0 && p.x < Repository.width && p.y >= 0 && p.y < Repository.height)
                                {
                                    var index = p.y * Repository.width + ((Repository.width - 1) - p.x);
                                    _grids[index].sprite = blocked ? _cellSpriteBlocked : _cellSpriteSelected;
                                    _grids[index].color = color;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void HighlightItem(IInventoryItem item)
        {
            if (item == null) return;
            ClearSelection();
            if (_renderMode == InventoryRenderMode.Single)
            {
                _grids[0].sprite = _cellSpriteSelected;
                _grids[0].color = Color.white;
                return;
            }
            for (var x = 0; x < item.width; x++)
            {
                for (var y = 0; y < item.height; y++)
                {
                    if (item.IsPartOfShape(new Vector2Int(x, y)))
                    {
                        var p = item.position + new Vector2Int(x, y);
                        if (p.x >= 0 && p.x < Repository.width && p.y >= 0 && p.y < Repository.height)
                        {
                            var index = p.y * Repository.width + ((Repository.width - 1) - p.x);
                            _grids[index].sprite = _cellSpriteHovered;
                            _grids[index].color = Color.white;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears all selections made in this inventory
        /// </summary>
        public void ClearSelection()
        {
            for (var i = 0; i < _grids.Length; i++)
            {
                _grids[i].sprite = _cellSpriteEmpty;
                _grids[i].color = Color.white;
            }
        }

        /*
        Returns the appropriate offset of an item to make it fit nicely in the grid
        */
        internal Vector2 GetItemOffset(IInventoryItem item)
        {
            var x = (-(Repository.width * 0.5f) + item.position.x + item.width * 0.5f) * cellSize.x;
            var y = (-(Repository.height * 0.5f) + item.position.y + item.height * 0.5f) * cellSize.y;
            return new Vector2(x, y);
        }
    }
}