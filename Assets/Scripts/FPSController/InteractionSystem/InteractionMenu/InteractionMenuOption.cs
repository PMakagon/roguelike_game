using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.FPSController.InteractionSystem.InteractionMenu
{
    [RequireComponent(typeof(Image))]
    public class InteractionMenuOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionName;
        [SerializeField] private Slider progressBar;

        [SerializeField] private Image target;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite selectSprite;
        [SerializeField] private Sprite confirmSprite;
        [SerializeField] private Sprite errorSprite;
        private Interaction _representedInteraction= null;
        private bool _isSelected;
        private int _menuIndex;

        public Interaction RepresentedInteraction => _representedInteraction;

        public Slider ProgressBar => progressBar;

        private void Awake()
        {
            if (target == null)  target = GetComponent<Image>();
        }

        public void Select()
        {
            target.sprite = selectSprite;
            _isSelected = true;
        }
        
        public void UnSelect()
        {
            target.sprite = defaultSprite;
            _isSelected = false;
        }

        public void ConfirmSelection()
        {
            var result = _representedInteraction.ActionOnInteract.Invoke();
           target.sprite = result ? confirmSprite : errorSprite;
           StartCoroutine(OnConfirm());
           Update();
        }

        public void Setup(Interaction interaction)
        {
            optionName.text = interaction.Label;
            _representedInteraction = interaction;

        }
        
        public void Update()
        {
            optionName.text = _representedInteraction.Label;
            if (!_isSelected) target.sprite = defaultSprite;//??
        }
        
        public void ResetProgressBar()
        {
            progressBar.gameObject.SetActive(false);
            progressBar.value = 0f;
        }

        public void SetProgressBarActive()
        {
            progressBar.gameObject.SetActive(true);
        }
        
        public void UpdateProgressBar(float fillAmount)
        {
            if (!progressBar.gameObject.activeSelf)  progressBar.gameObject.SetActive(true);
            progressBar.value = fillAmount;
        }

        private IEnumerator OnConfirm()
        {
            var currentSprite = target.sprite;
            yield return new WaitForSeconds(0.1f);
            target.sprite = selectSprite;
            yield return new WaitForSeconds(0.1f);
            target.sprite = currentSprite;
            yield return new WaitForSeconds(0.1f);
            target.sprite = selectSprite;
            // yield return new WaitForSeconds(0.1f);
            // target.sprite = currentSprite;
        }
    }
}