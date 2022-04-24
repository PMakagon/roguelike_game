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

    private Dictionary<string, Transform> levels = new Dictionary<string, Transform>();

    public bool UpdateCode;
    private string nextLevelCode;

    private void Awake()
    {
        liftControllerData.CurrentLevel = currentLevelStartPoint.transform;
        nextLevelCode= "Not Created";
        CreateNextLevelCode();
    }

    public string NextLevelCode
    {
        get => nextLevelCode;
        set => nextLevelCode = value;
    }


    [ContextMenu("CREATE NEW LEVEL TRANSFORM")]
    private void CreateLevelStartPoint()
    {
        var dist = (liftControllerData.CurrentLevel.transform.position + Vector3.down*distanceToLevel);
        newLevelStartPoint = Instantiate(currentLevelStartPoint.gameObject, dist, Quaternion.identity).transform;
        liftControllerData.DestinationLevel = newLevelStartPoint;
        levelGenerator.StartPoint = newLevelStartPoint;
        currentLevelStartPoint = newLevelStartPoint;
        levels.Add(nextLevelCode,newLevelStartPoint);
        CreateNextLevelCode();
    }
    
    private void Update()
    {
        if (UpdateCode)
        {
            CreateNextLevelCode();
        }
        // if (liftControllerData.IsOnLevel)
        // {
        //     UpdateCode = true;
        // }

        if (liftControllerData.EnteredCombination.Equals(nextLevelCode))
        {
            liftControllerData.IsCodeEntered = true;
            CreateLevelStartPoint();
            levelGenerator.enableGeneration = true;
        }
    }

    private void CreateNextLevelCode()
    {
        UpdateCode = false;
        nextLevelCode=string.Empty;
        for (int i = 0; i <= 2; i++)
        {
            var randomInt = Random.Range(1, 9); 
            nextLevelCode += randomInt;
        }
      
    }
    
    
    
}
