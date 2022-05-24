using System.Collections.Generic;
using LiftStateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private Transform currentLevelStartPoint;
        [SerializeField] private Transform newLevelStartPoint;
        [Range(5f,20f)]
        [SerializeField] private float distanceBetweenLevels = 5f;
        private Dictionary<string, Transform> _levels;
        private string _nextLevelCode;
        private int _codeLenght = 3;

        public bool UpdateCode { get; set; }
        public bool SwitchLevel { get; set; }

        private void Awake()
        {
            liftControllerData.CurrentLevel = currentLevelStartPoint.transform;
            liftControllerData.StartLevel = liftControllerData.CurrentLevel;
            _nextLevelCode= "Not Created";
            _levels = new Dictionary<string, Transform>();
        }

        private void Start()
        {
            CreateLevelStartPoint();
        }

        private void Update()
        {
            if (UpdateCode)
            {
                CreateNextLevelCode();
            }

            if ( liftControllerData.IsCodeEntered)
            {
                CreateLevelStartPoint();
                //переместить в LevelManager
                levelGenerator.enableGeneration = true;
            
            }

            if (SwitchLevel)
            {
                SwitchLevel = false;
                if (!newLevelStartPoint)
                {
                    CreateLevelStartPoint();
                }
            }
        }

        [ContextMenu("CREATE NEW LEVEL TRANSFORM")]
        private void CreateLevelStartPoint()
        {
            var dist = (liftControllerData.CurrentLevel.transform.position + Vector3.up*distanceBetweenLevels);
            newLevelStartPoint = Instantiate(currentLevelStartPoint.gameObject, dist, Quaternion.identity).transform;
            liftControllerData.DestinationLevel = newLevelStartPoint;
            levelGenerator.StartPoint = newLevelStartPoint;
            currentLevelStartPoint = newLevelStartPoint;
            newLevelStartPoint = null;
            _levels.Add(_nextLevelCode,newLevelStartPoint);
            CreateNextLevelCode();
        }


        private void CreateNextLevelCode()
        {
            UpdateCode = false;
            _nextLevelCode=string.Empty;
            for (int i = 0; i < _codeLenght; i++)
            {
                var randomInt = Random.Range(1, 9); 
                _nextLevelCode += randomInt;
            }
            liftControllerData.NextLevelCode = _nextLevelCode;
        }

        public string NextLevelCode
        {
            get => _nextLevelCode;
            set => _nextLevelCode = value;
        }
    }
}
