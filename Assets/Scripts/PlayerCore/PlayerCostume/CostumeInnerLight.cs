using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;

namespace LiftGame.PlayerCore.PlayerCostume
{
    public class CostumeInnerLight : MonoBehaviour
    {
        private Light _light;

        private void Awake()
        {
            _light = GetComponent<Light>();
            PlayerPowerEventHolder.OnPowerOff += EnableRedLight;
            PlayerPowerEventHolder.OnPowerOn += EnableWhiteLight;
        }

        private void OnDestroy()
        {
            PlayerPowerEventHolder.OnPowerOff -= EnableRedLight;
            PlayerPowerEventHolder.OnPowerOn -= EnableWhiteLight;
        }

        private void EnableRedLight()
        {
            _light.color = Color.red;
        }
        private void EnableWhiteLight()
        {
            _light.color = Color.white;
        }
        
        
    }
}