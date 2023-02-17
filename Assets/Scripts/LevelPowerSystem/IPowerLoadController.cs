using System.Collections.Generic;

namespace LiftGame.LevelPowerSystem
{
    public interface IPowerLoadController 
    {
        public bool IsEnabled { get; set; }
        public List<IPowerLoad> ControlledLoad { get; set;}
    }
}