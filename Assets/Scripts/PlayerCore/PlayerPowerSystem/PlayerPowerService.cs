using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.GameCore.Pause;
using LiftGame.Inventory;
using LiftGame.ProxyEventHolders;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.PlayerPowerSystem
{
    public class PlayerPowerService : IPlayerPowerService
    {
        private readonly PlayerPowerData _playerPowerData;
        private IPlayerInventoryService _inventoryService;
        private IPauseHandler _pauseHandler;
        private CancellationTokenSource _cancellationToken = new();
        private bool _isPaused;
        private int _powerCellsCount;

        [Inject]
        public PlayerPowerService(IPlayerData playerData, IPlayerInventoryService inventoryService,
            IPauseHandler pauseHandler)
        {
            _playerPowerData = playerData.GetPowerData();
            _inventoryService = inventoryService;
            _pauseHandler = pauseHandler;
            LevelGameLoopEventHandler.OnLoopStart += Initialize;
            LevelGameLoopEventHandler.OnLoopEnd += Deinitialize;
        }

        private void SubscribeToPowerEvents()
        {
            PlayerPowerEventHolder.OnPowerOn += EnablePowerConsumption;
            PlayerPowerEventHolder.OnPowerOff += DisablePowerConsumption;
            PlayerPowerEventHolder.OnPowerDamageTaken += DamagePower;
            PlayerPowerEventHolder.OnPowerRestored += AddPower;
            PlayerPowerEventHolder.OnPowerCellAdded += AddPowerCell;
            PlayerPowerEventHolder.OnPowerCellRemoved += RemovePowerCell;
        }

        private void UnsubscribeFromPowerEvents()
        {
            PlayerPowerEventHolder.OnPowerOn -= EnablePowerConsumption;
            PlayerPowerEventHolder.OnPowerOff -= DisablePowerConsumption;
            PlayerPowerEventHolder.OnPowerDamageTaken -= DamagePower;
            PlayerPowerEventHolder.OnPowerRestored -= AddPower;
            PlayerPowerEventHolder.OnPowerCellAdded -= AddPowerCell;
            PlayerPowerEventHolder.OnPowerCellRemoved -= RemovePowerCell;
        }

        private void Initialize()
        {
            SubscribeToPowerEvents();
            CheckPowerCells();
            CalculateCurrentPower();
            Debug.Log("POWER SERVICE ENABLED");
        }

        private void Deinitialize()
        {
            UnsubscribeFromPowerEvents();
            ResetPowerData();
        }

        public void EnablePowerConsumption()
        {
            _playerPowerData.IsPowerOn = true;
            EnablePowerUpdate();
        }

        public void DisablePowerConsumption()
        {
            _playerPowerData.IsPowerOn = false;
        }

        private async void EnablePowerUpdate()
        {
            await UpdatePowerReduce(_cancellationToken.Token);
        }

        public float GetCurrentPower()
        {
            return _playerPowerData.CurrentPower;
        }

        public float GetCurrentLoad()
        {
            return _playerPowerData.PowerLoad;
        }

        public void AddLoad(float newLoad)
        {
            if (!_playerPowerData.IsPowerOn) return;
            _playerPowerData.PowerLoad += newLoad;
            PlayerPowerEventHolder.BroadcastOnLoadChanged(_playerPowerData);
        }

        public void AddLoad(float newLoad, float penalty)
        {
            if (!_playerPowerData.IsPowerOn) return;
            DamagePower(penalty);
            _playerPowerData.PowerLoad += newLoad;
            PlayerPowerEventHolder.BroadcastOnLoadChanged(_playerPowerData);
        }

        public void RemoveLoad(float load)
        {
            _playerPowerData.PowerLoad -= load;
            if (_playerPowerData.PowerLoad<_playerPowerData.ConstLoad)
            {
                _playerPowerData.PowerLoad = _playerPowerData.ConstLoad;
            }
            PlayerPowerEventHolder.BroadcastOnLoadChanged(_playerPowerData);
        }

        public void AddPower(float powerToAdd)
        {
            if (!_playerPowerData.IsPowerOn) return;
            var powerPerCell = powerToAdd / _powerCellsCount;
            foreach (var repo in _inventoryService.GetPowerCellSlotRepository())
            {
                if (repo.IsEmpty) continue;
                var powerCell = repo.GetPowerCellItem();
                if (!powerCell.IsEmpty())
                {
                    powerCell.CurrentPower += powerPerCell;
                }
            }
            if (IsNoPower() && !_playerPowerData.IsPowerOn)
            {
                PlayerPowerEventHolder.BroadcastOnPowerOn();
            }

            PlayerPowerEventHolder.BroadcastOnPowerRestoreApplied(_playerPowerData, powerToAdd);
            HandleOverload();
        }

        public void DamagePower(float powerDamage)
        {
            if (!_playerPowerData.IsPowerOn) return;
            var damagePerCell = powerDamage / _powerCellsCount;
            foreach (var repo in _inventoryService.GetPowerCellSlotRepository())
            {
                if (repo.IsEmpty) continue;
                var powerCell = repo.GetPowerCellItem();
                if (!powerCell.IsEmpty())
                {
                    powerCell.CurrentPower -= damagePerCell;
                }
            }
            PlayerPowerEventHolder.BroadcastOnPowerDamageApplied(_playerPowerData, powerDamage);
        }

        public bool IsNoPower() => _playerPowerData.CurrentPower <= _playerPowerData.MinPower;

        private void HandleOverload()
        {
            if (_playerPowerData.CurrentPower > _playerPowerData.MaxPower)
            {
                _playerPowerData.CurrentPower = _playerPowerData.MaxPower;
            }
        }

        private async UniTask UpdatePowerReduce(CancellationToken cancelToken)
        {
            while (_playerPowerData.IsPowerOn)
            {
                if (_isPaused)
                {
                    await UniTask.WaitUntil(() => _isPaused == false, cancellationToken: cancelToken);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_playerPowerData.ReduceRate), ignoreTimeScale: false,
                    cancellationToken: cancelToken);
                ApplyPowerLoad();
                CalculateCurrentPower();
            }
        }

        private void CheckPowerCells()
        {
            float capacitySum = 0;
            int count = 0;
            foreach (var repo in _inventoryService.GetPowerCellSlotRepository())
            {
                if (repo.IsEmpty) continue;
                capacitySum += repo.GetPowerCellItem().MaxCapacity;
                count++;
            }

            _powerCellsCount = count;
            var maxPowerCachedValue = _playerPowerData.MaxPower;
            _playerPowerData.MaxPower = capacitySum;
            if (maxPowerCachedValue != capacitySum) PlayerPowerEventHolder.BroadcastOnMaxPowerChanged(_playerPowerData);
            if (_powerCellsCount == 0 && _playerPowerData.IsPowerOn)
            {
                PlayerPowerEventHolder.BroadcastOnPowerOff();
            }
        }

        private void CalculateCurrentPower()
        {
            float powerSum = 0;
            foreach (var repo in _inventoryService.GetPowerCellSlotRepository())
            {
                if (repo.IsEmpty) continue;
                var powerCell = repo.GetPowerCellItem();
                if (powerCell.IsEmpty())
                {
                    powerCell.BecomeEmpty();
                    continue;
                }

                powerSum += powerCell.CurrentPower;
            }

            _playerPowerData.CurrentPower = powerSum;
            PlayerPowerEventHolder.BroadcastOnPowerChanged(_playerPowerData);
            if (_playerPowerData.IsPowerOn)
            {
                if (_playerPowerData.MaxPower == 0 || _playerPowerData.CurrentPower == 0)
                {
                    PlayerPowerEventHolder.BroadcastOnPowerOff();
                }
            }
            else
            {
                if (_playerPowerData.CurrentPower == 0) return;
                PlayerPowerEventHolder.BroadcastOnPowerOn();
            }
        }

        private float CalculateLoadOnEachCell()
        {
            var partialLoad = _playerPowerData.PowerLoad / _powerCellsCount;
            var emptyPowerCells = 0;
            foreach (var repo in _inventoryService.GetPowerCellSlotRepository())
            {
                if (repo.IsEmpty) continue;
                var powerCell = repo.GetPowerCellItem();
                if (powerCell.IsEmpty())
                {
                    emptyPowerCells++;
                    partialLoad = _playerPowerData.PowerLoad / _powerCellsCount - emptyPowerCells;
                }
            }
            return partialLoad;
        }

        private void ApplyPowerLoad()
        {
            if (!_playerPowerData.IsPowerOn) return;
            if (IsNoPower())
            {
                PlayerPowerEventHolder.BroadcastOnPowerOff();
            }
            else
            {
                var loadOnCell = CalculateLoadOnEachCell();
                foreach (var repo in _inventoryService.GetPowerCellSlotRepository())
                {
                    if (repo.IsEmpty) continue;
                    var powerCell = repo.GetPowerCellItem();
                    if (!powerCell.IsEmpty())
                    {
                        powerCell.CurrentPower -= loadOnCell;
                    }
                }
            }
        }

        private void AddPowerCell(int slotId)
        {
            CheckPowerCells();
            CalculateCurrentPower();
        }

        private void RemovePowerCell(int slotId)
        {
            CheckPowerCells();
            CalculateCurrentPower();
        }

        public void ResetPowerData()
        {
            _playerPowerData.ResetData();
            PlayerPowerEventHolder.BroadcastOnPowerChanged(_playerPowerData);
        }

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public PlayerPowerData PlayerPowerData => _playerPowerData;
    }
}