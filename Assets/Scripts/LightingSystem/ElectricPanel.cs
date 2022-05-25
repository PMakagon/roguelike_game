using UnityEngine;

namespace LightingSystem
{
    public class ElectricPanel : MonoBehaviour
    {
        [SerializeField] private MasterSwitcher[] masterSwitchers;

        public MasterSwitcher[] MasterSwitchers
        {
            get => masterSwitchers;
            set => masterSwitchers = value;
        }


        private void OnInteract()
        {
        
        }
    }
}
