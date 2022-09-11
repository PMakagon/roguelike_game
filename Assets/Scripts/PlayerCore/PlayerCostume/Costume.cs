using UnityEngine;

namespace LiftGame.PlayerCore.PlayerCostume
{
    [CreateAssetMenu(fileName = "Costume", menuName = "Costume")]
    public class Costume : ScriptableObject
    {
        public GameObject visor;
        public GameObject headMesh;
    }
}