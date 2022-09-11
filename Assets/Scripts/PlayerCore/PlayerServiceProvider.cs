
using LiftGame.FPSController;
using LiftGame.FPSController.CameraController;
using LiftGame.FPSController.FirstPersonController;
using LiftGame.GameCore.Input;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerCostume;
using UnityEngine;

namespace LiftGame.PlayerCore
{
    public class PlayerServiceProvider : MonoBehaviour
    {
        [SerializeField] private FirstPersonController fpsController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerCostumeService playerCostumeService;//убрать
        [SerializeField] private PlayerLitStateProvider playerLitStateProvider;
        
        
        public FirstPersonController FPSController => fpsController;
        public CameraController CameraController => cameraController;
        public PlayerAnimationController PlayerAnimationController => playerAnimationController;
        public PlayerCostumeService PlayerCostumeService => playerCostumeService;
        public PlayerLitStateProvider PlayerLitStateProvider => playerLitStateProvider;
    }
}