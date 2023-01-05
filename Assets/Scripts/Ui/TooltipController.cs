using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.Input.Data;
using LiftGame.Inventory.Core;
using UnityEngine;

namespace LiftGame.Ui
{
    public class TooltipController : MonoBehaviour
    {
        [SerializeField] private InventoryMouseTooltip tooltip;
        [SerializeField] private float delay;
        private static float _staticDelay;
        private static CancellationTokenSource _cancellationToken = new();

        private static TooltipController _currentTooltipController;

        private void Awake()
        {
            if (!_currentTooltipController)
            {
                _currentTooltipController = this;
            }

            _staticDelay = delay;
            UIInputData.OnInventoryClicked += Hide;
            NonGameplayInputData.OnPauseMenuClicked += Hide;
        }

        private void OnDestroy()
        {
            UIInputData.OnInventoryClicked -= Hide;
            NonGameplayInputData.OnPauseMenuClicked -= Hide;
        }

        private void Update()
        {
            if (tooltip.enabled) _currentTooltipController.tooltip.transform.position = Input.mousePosition;
        }

        public static async void Show(IInventoryItem item)
        {
            if (item == null) return;
            _cancellationToken = new CancellationTokenSource();
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_staticDelay), ignoreTimeScale: false, cancellationToken: _cancellationToken.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            _currentTooltipController.tooltip.SetTooltip(item);
            _currentTooltipController.tooltip.transform.position = Input.mousePosition;
            _currentTooltipController.tooltip.gameObject.SetActive(true);
        }

        public static void Hide()
        {
            _cancellationToken.Cancel();
            _currentTooltipController.tooltip.gameObject.SetActive(false);
        }
    }
}