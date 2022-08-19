using System.Globalization;
using LiftGame.PlayerCoreMechanics.PlayerPowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui.HUD
{
    public class PowerUIBar: MonoBehaviour

    {
        [SerializeField] private Text power;
        [SerializeField] private Text currentLoad;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Image status;
        [SerializeField] private PlayerPowerData playerPowerData;

        private void Start()
        {
            playerPowerData.onPowerChange += UpdatePowerBar;
        }

        private void UpdatePowerBar()
        {
            float currentPower = playerPowerData.CurrentPower;
            power.text = currentPower.ToString(CultureInfo.CurrentCulture);
            if (currentPower <=0)
            {
                status.sprite = sprites[6];
                return;
            }
            if (currentPower <100f && currentPower >=90f)
            {
                status.sprite = sprites[0];
                return;
            }
            if (currentPower <90f && currentPower >=75f)
            {
                status.sprite = sprites[1];
                return;
            }
            if (currentPower <75f && currentPower >=50f)
            {
                status.sprite = sprites[2];
                return;
            }
            if (currentPower <50f && currentPower >=25f)
            {
                status.sprite = sprites[3];
                return;
            }
            if (currentPower <25f && currentPower >=10)
            {
                status.sprite = sprites[4];
                return;
            }
            if (currentPower <10f && currentPower >0)
            {
                status.sprite = sprites[5];
                return;
            }
        }
    
        private void Update()
        {
            power.text = playerPowerData.CurrentPower.ToString(CultureInfo.CurrentCulture);
            currentLoad.text ="load " + playerPowerData.PowerLoad.ToString(CultureInfo.CurrentCulture);
        }
    }
}