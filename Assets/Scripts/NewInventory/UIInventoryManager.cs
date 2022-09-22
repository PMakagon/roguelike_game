using LiftGame.GameCore.Input.Data;
using LiftGame.NewInventory.Container;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.NewInventory
{
    public class UIInventoryManager : MonoBehaviour
    {
        [SerializeField] private ContainerPanelController containerController;
        [SerializeField] private GameObject containerPanel;
        private bool _isContainerPanelActive = false;
        private InventoryData _inventoryData;
        
        [Inject]
        private void Construct(IPlayerData playerData)
        {
            _inventoryData = playerData.GetInventoryData();
        }
        
        void Start()
        {
            _inventoryData.onContainerOpen += OpenContainerPanel;
            UIInputData.OnInventoryClicked += CloseInventory;
        }

        private void CloseInventory()
        {
            if (_isContainerPanelActive)
            {
                _isContainerPanelActive = false;
                containerPanel.SetActive(_isContainerPanelActive);
            }   
        }
        

        private void OpenContainerPanel()
        {
            _isContainerPanelActive = true;
            containerPanel.SetActive(_isContainerPanelActive);
        }
    }
}