using LiftGame.InteractableObjects.Electricals;
using UnityEngine;

namespace LiftGame.LevelPowerSystem
{
    public class ElectricPanel : MonoBehaviour
    {
        [SerializeField] private MasterSwitcher[] masterSwitchers;

        public MasterSwitcher[] MasterSwitchers
        {
            get => masterSwitchers;
            set => masterSwitchers = value;
        }
        
    }
}
