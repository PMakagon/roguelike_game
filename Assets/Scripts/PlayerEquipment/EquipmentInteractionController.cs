using InventorySystem;
using PlayerPowerSystem;
using UnityEngine;

namespace PlayerEquipment
{
    public class EquipmentInteractionController : MonoBehaviour
    {
        [SerializeField] private EquipmentInputData inputData;
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] private PowerData powerData;
        private HeadFlashlight _headFlashlight;
        
        private void Awake()
        {
            _headFlashlight = GetComponentInChildren<HeadFlashlight>();
            _headFlashlight.PowerData = powerData;
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
                    if (powerEquipment.PowerData==null)
                    {
                        powerEquipment.PowerData = powerData;
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