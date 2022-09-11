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
        
        [Inject]
        private void Construct(IPlayerData playerData)
        {
            _playerMentalData = playerData.GetMentalData();
        }

        private void Update()
        {
            stressMod.text = _playerMentalData.CurrentStressModificator.ToString();
            stress.text = _playerMentalData.Stress.ToString();
            stressState.text =  "StressState: " + _playerMentalData.StressState;
        }
    }
}