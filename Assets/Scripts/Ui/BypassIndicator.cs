using System;
using LiftGame.GameCore.Input.Data;
using LiftGame.PlayerCore.PlayerAirSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiftGame.Ui
{
    public class BypassIndicator : MonoBehaviour
    {
        [SerializeField] private Sprite openIcon;
        [SerializeField] private Sprite closeIcon;
        private IPlayerAirService _airService;
        private Image _icon;
        
        //MonoBehaviour injection
        [Inject]
        public void Construct(IPlayerAirService airService)
        {
            _airService = airService;
        }

        private void Awake()
        {
            _icon = GetComponent<Image>();
        }

        private void Start()
        {
            EquipmentInputData.OnAirBypassClicked += IconChange;
            IconChange();
        }

        private void OnDestroy()
        {
            EquipmentInputData.OnAirBypassClicked -= IconChange;
        }

        private void IconChange()
        {
            _icon.sprite = _airService.IsBypassed() ? openIcon : closeIcon;
        }
    }
}