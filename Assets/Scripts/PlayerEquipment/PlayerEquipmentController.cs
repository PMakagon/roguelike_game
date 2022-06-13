using System;
using System.Collections.Generic;
using PlayerPowerSystem;
using UnityEngine;

namespace PlayerEquipment
{
    public class PlayerEquipmentController : MonoBehaviour

    {
        [SerializeField] private EquipmentInputData inputData;
        [SerializeField] private PowerData powerData;
        private List<IPlayerEquipment> _equipments;
        private IPlayerEquipment currentEquipment;
        private HeadFlashlight _headFlashlight;

        private void Awake()
        {
            _equipments = new List<IPlayerEquipment>();
            _headFlashlight = GetComponentInChildren<HeadFlashlight>();
            _headFlashlight.PowerData = powerData;
        }

        private void OnPickUp(IPlayerEquipment equipment)
        {
            
        }

        private void ChangeEquipment()
        {
            //упростить через int в параметрах и метод в инпуте
            if (inputData.FirstEquipmentClicked)
            {
                OnEquip(_equipments[0]);
                return;
            }

            if (inputData.SecondEquipmentClicked)
            {
                OnEquip(_equipments[1]);
                return;
            }
            
            if (inputData.SecondEquipmentClicked)
            {
                OnEquip(_equipments[2]);
                return;
            }
        }


        private void Update()
        {
            if (inputData.UsingClicked)
            {
                OnPlayerUse();
            }

            if (inputData.TurnOnClicked)
            {
                if (!currentEquipment.IsTurnedOn)
                {
                    OnTurningOn();
                }
                else
                {
                    OnTurningOff();
                }
            }
            
            if (inputData.FlashlightClicked)
            {
                _headFlashlight.SwitchState = true;
                Debug.Log("VAR");
            }
        }

        private void OnPlayerUse()
        {
            currentEquipment?.Use();
        }

        private void OnEquip(IPlayerEquipment chosenEquipment)
        {
            currentEquipment?.UnEquip();
            currentEquipment = chosenEquipment;
        }

        private void OnTurningOn()
        {
            currentEquipment?.TurnOn();
        }
        
        private void OnTurningOff()
        {
            currentEquipment?.TurnOff();
        }
        
        
    }
}