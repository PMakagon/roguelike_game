using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.PlayerCostume
{
    public class PlayerCostumeService : MonoBehaviour, IPlayerCostumeService
    {
        
        [SerializeField] private Costume currentCostume;
        [SerializeField]private GameObject _visor;
        [SerializeField]private GameObject _headMesh;

        
        public void SetCostumeActive(bool state)
        {
            _visor.SetActive(state);
            _headMesh.SetActive(state);
        }

        public void SetVisorActive(bool state)
        {
            _visor.SetActive(state);
        }
        
        public void PutOnCostume()
        {
            _visor.SetActive(true);
            _headMesh.SetActive(true);
        }

        private void ChangeCostume()
        {
            _visor = currentCostume.visor;
            _headMesh = currentCostume.headMesh;
        }

        public Costume CurrentCostume
        {
            get => currentCostume;
            set
            {
                currentCostume = value;
                // PutOnCostume();
            }
        }
    }
}