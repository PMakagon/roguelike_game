using System;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.ProxyEventHolders;
using UnityEngine;

namespace LiftGame.FX
{
    public class PlayerHUDFxController : MonoBehaviour
    {
        private void Start()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            PlayerHealthEventHolder.OnDamageApplied += ApplyHealthDamageFX;
            PlayerMentalEventHolder.OnStressApplied += ApplyStressDamageFX;
        }

        private void Unsubscribe()
        {
            PlayerHealthEventHolder.OnDamageApplied -= ApplyHealthDamageFX;
            PlayerMentalEventHolder.OnStressApplied -= ApplyStressDamageFX;

        }

        private void ApplyStressDamageFX(PlayerMentalData arg1, int arg2)
        {
            
        }

        private void ApplyHealthDamageFX(PlayerHealthData playerHealthData, int damage)
        {
            if (damage > playerHealthData.MaxStressDamage)
            {
                DistortHUD(damage);
            }
        }

        private void DistortHUD(float velocity)
        {
            Debug.Log("DAMAGE ON HUD");
        }
    }
}