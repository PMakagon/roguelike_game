using System.Collections.Generic;
using UnityEngine;

namespace LiftGame.LevelCore.LevelPowerSystem
{
    public class LevelPowerData : ScriptableObject
    {
        private List<IPowerControlledEntity> _controlledEntities = new List<IPowerControlledEntity>();

        public List<IPowerControlledEntity> ControlledEntities => _controlledEntities;

        public bool IsPowerEnabled { get; set; }

        public void ResetData()
        {
            IsPowerEnabled = false;
            _controlledEntities.Clear();
        }
    }
}