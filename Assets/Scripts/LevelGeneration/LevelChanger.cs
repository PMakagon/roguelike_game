using System.Collections.Generic;
using LiftStateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private LevelGenerationManager levelGenerationManager;
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private Transform currentLevelStartPoint;
        [SerializeField] private Transform newLevelStartPoint;
        [Range(5f,20f)]
        [SerializeField] private float distanceBetweenLevels = 5f;
        private Dictionary<string, Transform> _levels;
        private string _nextLevelCode;
        private int _codeLenght = 3;
        private int _levelCount;

        public bool UpdateCode { get; set; }
        public bool TestSwitchLevel { get; set; }

        private void Awake()
        {
            liftControllerData.CurrentLevel = currentLevelStartPoint.transform; // бред
            liftControllerData.StartLevel = liftControllerData.CurrentLevel;
            _nextLevelCode= "Not Created";
            _levels = new Dictionary<string, Transform>();
            liftControllerData.OnCodeEntered += InitializeLevelChange;
            LiftControllerData.OnLevelGameLoopFinished += CreateNextLevelCode;
        }
        private void OnDisable()
        {
            liftControllerData.OnCodeEntered -= InitializeLevelChange;
        }
        
        //убрать
        private void Start()
        {
            CreateLevelStartPoint();
            CreateNextLevelCode();
        }

        private void InitializeLevelChange()
        {
            if (!newLevelStartPoint)
            {
                CreateLevelStartPoint();
            }
            //переместить в LevelManager
            _levelCount += 1;
            levelGenerationManager.StartGeneration=true;
            liftControllerData.IsCodeEntered = true;
        }
        
        private void Update()
        {
            
            if (UpdateCode)
            {
                CreateNextLevelCode();
            }

            // if ( liftControllerData.IsCodeEntered)
            // {
            //     if (!newLevelStartPoint)
            //     {
            //         CreateLevelStartPoint();
            //     }
            //     //переместить в LevelManager
            //     _levelCount += 1;
            //     levelGenerationManager.StartGeneration=true;
            // }
            
            //для тестов

            if (TestSwitchLevel)
            {
                TestSwitchLevel = false;
                if (!newLevelStartPoint)
                {
                    // CreateLevelStartPoint();
                    var dist = (currentLevelStartPoint.transform.position + Vector3.up*distanceBetweenLevels);
                    newLevelStartPoint = Instantiate(currentLevelStartPoint.gameObject, dist, Quaternion.identity).transform;
                    levelGenerator.StartPoint = newLevelStartPoint;
                    currentLevelStartPoint = newLevelStartPoint;
                    newLevelStartPoint = null;
                    CreateNextLevelCode();
                }
                _levelCount += 1;
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
            // CreateNextLevelCode();
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

        public int LevelCount
        {
            get => _levelCount;
            set => _levelCount = value;
        }
    }
}
