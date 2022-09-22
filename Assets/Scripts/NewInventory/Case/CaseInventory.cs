using UnityEngine;

namespace LiftGame.NewInventory.Case
{
    [RequireComponent(typeof(BoxCollider))]
    public class CaseInventory : MonoBehaviour
    {
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] [Range(1,6)]private int widht;
        [SerializeField] [Range(1,5)] private int height;

        private void Awake()
        {
            inventoryData.CaseInventory = new CaseItemProvider(widht, height);;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                inventoryData.CaseInventory.IsInRange = true;
                Debug.Log("CASE");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                inventoryData.CaseInventory.IsInRange = false;
                Debug.Log("NO CASE");
            }
        }
    }
}