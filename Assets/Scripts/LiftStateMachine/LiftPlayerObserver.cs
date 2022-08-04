using System;
using UnityEngine;

namespace LiftStateMachine
{
    public class LiftPlayerObserver : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        private bool _toggleSearch = true;

        private void Awake()
        {
            LiftControllerData.OnPlayerEnteredNewLevel += EnableSearch;
            LiftControllerData.OnLevelGameLoopFinished += EnableSearch;
        }

        private void Update()
        {
            if (_toggleSearch)
            {
                if (liftControllerData.IsPlayerLeft && liftControllerData.IsOnLevel)
                {
                    liftControllerData.OnPlayerLeftLiftZone.Invoke();
                    _toggleSearch = false;
                }
            }
        }

        private void EnableSearch()
        {
            _toggleSearch = true;
        } 
        
        private void OnDisable()
        {
            LiftControllerData.OnPlayerEnteredNewLevel -= EnableSearch;
        }
        
    }
}
