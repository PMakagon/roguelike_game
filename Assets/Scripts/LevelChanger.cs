using System;
using System.Collections.Generic;
using LevelGeneration;
using LiftStateMachine;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private LiftControllerData liftControllerData;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private Transform currentLevelStartPoint;
    [SerializeField] private Transform newLevelStartPoint;
    [Range(5f,20f)]
    [SerializeField] private float distanceToLevel = 5f;
    private Dictionary<string, Transform> _levels;
    public bool updateCode;
    private string _nextLevelCode;
    private int _codeLenght = 3;

    private void Awake()
    {
        liftControllerData.CurrentLevel = currentLevelStartPoint.transform;
        _nextLevelCode= "Not Created";
        CreateNextLevelCode();
        _levels = new Dictionary<string, Transform>();
    }

    private void Update()
    {
        if (updateCode)
        {
            CreateNextLevelCode();
        }

        if ( liftControllerData.IsCodeEntered)
        {
            if (!liftControllerData.DestinationLevel)
            {
                CreateLevelStartPoint();
                levelGenerator.enableGeneration = true;
            }
        }
    }

    [ContextMenu("CREATE NEW LEVEL TRANSFORM")]
    private void CreateLevelStartPoint()
    {
        var dist = (liftControllerData.CurrentLevel.transform.position + Vector3.up*distanceToLevel);
        newLevelStartPoint = Instantiate(currentLevelStartPoint.gameObject, dist, Quaternion.identity).transform;
        liftControllerData.DestinationLevel = newLevelStartPoint;
        levelGenerator.StartPoint = newLevelStartPoint;
        currentLevelStartPoint = newLevelStartPoint;
        _levels.Add(_nextLevelCode,newLevelStartPoint);
        CreateNextLevelCode();
    }


    private void CreateNextLevelCode()
    {
        updateCode = false;
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
