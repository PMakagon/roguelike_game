using LiftGame.LiftStateMachine;
using TMPro;
using UnityEngine;

namespace LiftGame
{
    public class LevelCodeTableTest : MonoBehaviour
    {
        [SerializeField] private LiftControllerData _liftControllerData;
        private TextMeshPro levelCodeText;
        

        private void Awake()
        {
            levelCodeText = GetComponentInChildren<TextMeshPro>();
        }

        private void Start()
        {
            levelCodeText.text = _liftControllerData.NextLevelCode;
        }

        private void Update()
        {
            if (!_liftControllerData.NextLevelCode.Equals(levelCodeText.text))
            {
                levelCodeText.text = _liftControllerData.NextLevelCode;
            }
        }
    }
}