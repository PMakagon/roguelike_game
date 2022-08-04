using System;
using System.Collections;
using PlayerEquipment;
using UnityEngine;

namespace PlayerPowerSystem
{
        public class PowerUpdater : MonoBehaviour
        {
                [SerializeField] private PowerData powerData;
                [SerializeField] private float reduceRate = 1f;

                private IEnumerator ReducePowerOverTime()
                {
                        while (powerData.IsPowerOn)
                        { 
                                yield return new WaitForSecondsRealtime(reduceRate);
                                powerData.ChangePower();
                        }
                }
                
                private void Start()
                {
                        powerData.ResetData();
                        if (powerData.IsPowerOn)
                        {
                                StartCoroutine(nameof(ReducePowerOverTime));
                        }
                }


                public void TESTCHARGEBATTERY(float charge)
                {
                        powerData.CurrentPower += charge;
                }
                public void TESTCHARGEBATTERYFULL()
                {
                        powerData.ResetData();
                }
                
        }
}