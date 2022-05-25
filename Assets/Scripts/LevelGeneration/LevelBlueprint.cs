using System.Collections.Generic;

namespace LevelGeneration
{
    public class LevelBlueprint 
    {
        public RootRoom CurrentRoot { get; set; }
        public List<Room> SpawnedRooms { get; set; }

        //параметры LevelGenerator
        public LevelGenerator.LevelType LevelType { get; set; }
        public int LevelSize { get; set; }
        public int BossRoomsCount { get; set; }

        //параметры doorSpawner
        public bool HasElectricDoors { get; set; }
        
        //параметры ElectricSpawner
        public bool HasGenerator { get; set; }
        public float LightOverloadLevel { get; set; }
        public float DarknessLevel { get; set; }
        public bool HasElectricPanel { get; set; }
        
        //Параметры EventSpawner
        public int JumpscareCount { get; set; }

        //параметры NPCSpawner
        public bool HasRootBoss { get; set; }
        
    }
}