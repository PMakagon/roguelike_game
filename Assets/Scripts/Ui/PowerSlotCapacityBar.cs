using LiftGame.Inventory.Items;
using LiftGame.Inventory.PowerCellSlots;
using LiftGame.PlayerCore.PlayerPowerSystem;
using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui
{
    public class PowerSlotCapacityBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;
        [SerializeField] private Color lowColor;
        [SerializeField] private Color midColor;
        [SerializeField] private Color maxColor;
        private IPowerCellSlotRepository _powerCellSlotRepository;

        public void SetupPowerCellCapacityBar(PowerCellSlotRepositoryManager powerCellSlotRepository)
        {
            _powerCellSlotRepository = powerCellSlotRepository.Repository;
            _slider.gameObject.SetActive(false);
        }

        private void Start()
        {
            PlayerPowerEventHolder.OnPowerCellAdded += InitCapacityBar;
        }

        private void OnDestroy()
        {
            PlayerPowerEventHolder.OnPowerCellAdded -= InitCapacityBar;
            PlayerPowerEventHolder.OnPowerCellRemoved -= HideCapacityBar;
            PlayerPowerEventHolder.OnPowerChanged -= UpdateCapacityBar;
        }

        private void UpdateCapacityBar(PlayerPowerData powerData)
        {
            if (_powerCellSlotRepository.IsEmpty) return;
            var powerCell = _powerCellSlotRepository.GetPowerCellItem();
            _slider.value = powerCell.CurrentPower;
            UpdateBarColor(powerCell);
        }

        private void UpdateBarColor(PowerCell cell)
        {
            if (cell.CurrentPower >= cell.MaxCapacity*0.7)
            {
                _fill.color = maxColor;
            } 
            if (cell.CurrentPower < cell.MaxCapacity*0.7)
            {
                _fill.color = midColor;
            } 
            if (cell.CurrentPower <= cell.MaxCapacity*0.3)
            {
                _fill.color = lowColor;
            }
            // _fill.color = Color.Lerp(_fill.color, targetColor, 1);
        }

        private void InitCapacityBar(int slotId)
        {
            if (slotId != _powerCellSlotRepository.SlotId) return;
            _slider.gameObject.SetActive(true);
            var powerCell = _powerCellSlotRepository.GetPowerCellItem();
            _slider.maxValue = powerCell.MaxCapacity;
            _slider.value = powerCell.CurrentPower;
            PlayerPowerEventHolder.OnPowerChanged += UpdateCapacityBar;
            PlayerPowerEventHolder.OnPowerCellRemoved += HideCapacityBar;
        }

        private void HideCapacityBar(int slotId)
        {
            if (slotId != _powerCellSlotRepository.SlotId) return;
            _slider.gameObject.SetActive(false);
            PlayerPowerEventHolder.OnPowerChanged -= UpdateCapacityBar;
            PlayerPowerEventHolder.OnPowerCellRemoved -= HideCapacityBar;
        }
    }
}