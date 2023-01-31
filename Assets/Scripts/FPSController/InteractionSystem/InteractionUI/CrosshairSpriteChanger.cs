using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.FPSController.InteractionSystem.InteractionUI
{
    public enum CrosshairType
    {
        Hand,
        Equipment,
        Aim,
        Dot
    }
    [RequireComponent(typeof(Image))]
    public class CrosshairSpriteChanger : MonoBehaviour
    {
        [SerializeField] private Sprite handSprite;
        [SerializeField] private Sprite equipmentSprite;
        [SerializeField] private Sprite aimSprite;
        [SerializeField] private Sprite dotSprite;
        private Image _crosshairImage;

        private void Awake()
        {
            _crosshairImage = GetComponent<Image>();
        }

        public void ChangeCrosshair(CrosshairType crosshairType)
        {
            _crosshairImage.sprite = crosshairType switch
            {
                CrosshairType.Hand => handSprite,
                CrosshairType.Equipment => equipmentSprite,
                CrosshairType.Aim => aimSprite,
                CrosshairType.Dot => dotSprite,
                _ => _crosshairImage.sprite
            };
        }

        private void OnEnable()
        {
            _crosshairImage.sprite = dotSprite;
        }

        private void OnDisable()
        {
            
        }
    }
}