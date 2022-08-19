using LiftGame.PlayerCoreMechanics.MentalSystem;
using TMPro;
using UnityEngine;

namespace LiftGame.Ui.HUD
{
    public class MentalStatusTest : MonoBehaviour
    {
        [SerializeField] private PlayerMentalController _playerMentalController;
        [SerializeField] private TextMeshProUGUI stress;
        [SerializeField] private TextMeshProUGUI stressMod;
        [SerializeField] private TextMeshProUGUI stressState;

        private void Update()
        {
            stressMod.text = _playerMentalController.CurrentStressModificator.ToString();
            stress.text = _playerMentalController.Stress.ToString();
            stressState.text =  "StressState: " + _playerMentalController.StressState;
        }
    }
}