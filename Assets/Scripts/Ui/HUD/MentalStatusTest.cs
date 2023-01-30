using LiftGame.PlayerCore;
using LiftGame.PlayerCore.MentalSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace LiftGame.Ui.HUD
{
    public class MentalStatusTest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stress;
        [SerializeField] private TextMeshProUGUI stressMod;
        [SerializeField] private TextMeshProUGUI stressState;
        private PlayerMentalData _playerMentalData;

        public PlayerMentalData PlayerMentalData
        {
            set => _playerMentalData = value;
        }

        public void UpdateStatus()
        {
            stressMod.text = _playerMentalData.CurrentStressModificator.ToString();
            stress.text = _playerMentalData.StressLevel.ToString();
            stressState.text =  "StressState: " + _playerMentalData.StressState;
        }
    }
}