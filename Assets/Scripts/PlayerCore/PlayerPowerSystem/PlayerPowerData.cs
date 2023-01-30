using UnityEngine;

namespace LiftGame.PlayerCore.PlayerPowerSystem
{
    [CreateAssetMenu(fileName = "PlayerPowerData", menuName = "PlayerCoreMechanics/PowerData")]
    public class PlayerPowerData : ScriptableObject
    {
        [SerializeField] private float maxPower;
        [SerializeField] private float constLoad = 1f;
        [SerializeField] private float reduceRate = 1f;
        private const float _minPower = 0;
        public float ConstLoad => constLoad;

        public float CurrentPower { get; set; }

        public float PowerLoad { get; set; }

        public bool IsPowerOn { get; set; }

        public float MaxPower
        {
            get => maxPower;
            set => maxPower = value;
        }

        public float ReduceRate => reduceRate;

        public float MinPower => _minPower;

        public void ResetData()
        {
            CurrentPower = 0;
            MaxPower = 0;
            PowerLoad = constLoad;
            IsPowerOn = false;
        }
    }
}