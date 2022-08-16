using System;
using System.Collections;
using PlayerEquipment;
using UnityEngine;

namespace PlayerPowerSystem
{
        public class PowerUpdater : MonoBehaviour
        {
                [SerializeField] private PlayerPowerData playerPowerData;
                [SerializeField] private float reduceRate = 1f;

                private IEnumerator ReducePowerOverTime()
                {
                        while (playerPowerData.IsPowerOn)
                        { 
                                yield return new WaitForSecondsRealtime(reduceRate);
                                playerPowerData.ChangePower();
                        }
                }
                
                private void Start()
                {
                        playerPowerData.ResetData();
                        if (playerPowerData.IsPowerOn)
                        {
                                StartCoroutine(nameof(ReducePowerOverTime));
                        }
                }


                public void TESTCHARGEBATTERY(float charge)
                {
                        playerPowerData.CurrentPower += charge;
                }
                public void TESTCHARGEBATTERYFULL()
                {
                        playerPowerData.ResetData();
                }
                
        }
}