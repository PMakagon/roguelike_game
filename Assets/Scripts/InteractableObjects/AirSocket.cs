using System;
using System.Globalization;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using TMPro;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class AirSocket : Interactable
    {
        [SerializeField] private float airCapacity = 100f;
        [SerializeField] private TextMeshPro capacityTextMesh;
        private Interaction _toFillAir = new Interaction("Fill",5, true);

        private void Start()
        {
            capacityTextMesh.text = airCapacity.ToString(CultureInfo.InvariantCulture);
        }

        public override void BindInteractions()
        {
            _toFillAir.actionOnInteract = FillAir;
        }
        
        public override void AddInteractions()
        {
            Interactions.Add(_toFillAir);
        }

        private bool FillAir()
        {
            if (airCapacity!=0)
            {
                var airService = CachedServiceProvider.AirService;
                var spaceToFill = airService.GetMaxLevel() - airService.GetCurrentLevel();
                if (airCapacity<=spaceToFill)
                { 
                    PlayerAirSupplyEventHolder.SendOnAirRestored(airCapacity);
                    airCapacity = Mathf.Clamp(airCapacity - spaceToFill, 0, airCapacity);
                }
                else
                {
                    PlayerAirSupplyEventHolder.SendOnAirRestored(spaceToFill);
                    airCapacity -= spaceToFill;
                }
                // Debug.Log(spaceToFill + " "+ airCapacity);
                capacityTextMesh.text = Math.Round(airCapacity,2).ToString(CultureInfo.InvariantCulture);
                return true;
            }
            return false;
        }
        
    }
}