using System;
using LiftGame.Inventory;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.Ui.HUD
{
    public class HUDControllerTEMP : MonoBehaviour
    {
        [SerializeField] private MentalStatusTest mentalStatus;
        [SerializeField] private HealthStatusTest healthStatus;
        private IPlayerInventoryService _inventoryService;
        
        [Inject]
        private void Construct(IPlayerData playerData,IPlayerInventoryService inventoryService)
        {
            mentalStatus.PlayerMentalData = playerData.GetMentalData();
            healthStatus.PlayerHealthData= playerData.GetHealthData();
            _inventoryService = inventoryService;
        }

        private void Start()
        {
            _inventoryService.OnInventoryLoad += ShowHUD;
            _inventoryService.OnInventoryOpen += HideHUD;
            _inventoryService.OnInventoryClose += ShowHUD;
        }

        private void OnDestroy()
        {
            _inventoryService.OnInventoryLoad -= ShowHUD;
            _inventoryService.OnInventoryOpen -= HideHUD;
            _inventoryService.OnInventoryClose -= ShowHUD;
        }

        private void ShowHUD()
        {
            mentalStatus.gameObject.SetActive(true);
            healthStatus.gameObject.SetActive(true);
        }
        private void HideHUD()
        {
            mentalStatus.gameObject.SetActive(false);
            healthStatus.gameObject.SetActive(false);
        }

        private void Update()
        {
           mentalStatus.UpdateStatus();
           healthStatus.UpdateStatus();
        }
    }
}