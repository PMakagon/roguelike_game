using LiftGame.GameCore.Input.Data;
using LiftGame.InventorySystem;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    public class EquipmentInteractionController : MonoBehaviour
    {
        [SerializeField] private EquipmentInputData inputData;
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] private PlayerPowerData playerPowerData;
        private HeadFlashlight _headFlashlight;
        
        private void Awake()
        {
            _headFlashlight = GetComponentInChildren<HeadFlashlight>();
            _headFlashlight.PlayerPowerData = playerPowerData;
        }

        private void Update()
        {
            if (inputData.FlashlightClicked)
            {
                _headFlashlight.SwitchState = true;
            }
            
            // использование
            if (inventoryData.CurrentEquipment != null)
            {
                 if (!inventoryData.CurrentEquipment.IsEquipped) return;
                if (inputData.UsingClicked)
                {
                    OnPlayerUse();
                }
               
                if ( inventoryData.CurrentEquipment is IPowerEquipment powerEquipment)
                {
                    if (powerEquipment.PlayerPowerData==null)
                    {
                        powerEquipment.PlayerPowerData = playerPowerData;
                    }
                    
                    if (inputData.TurnOnClicked)
                    {
                        // вкл выкл
                        if (!powerEquipment.IsTurnedOn)
                        {
                            powerEquipment.TurnOn();
                        }
                        else
                        {
                            powerEquipment.TurnOff();
                        }
                    }
                }
            }
        }

        private void OnPlayerUse()
        {
            inventoryData.CurrentEquipment?.Use();
        }
    }
}