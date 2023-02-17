using LiftGame.FPSController.InteractionSystem.InteractionUI;
using LiftGame.GameCore.Input;
using LiftGame.GameCore.Input.Data;
using LiftGame.Inventory;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerAirSystem;
using LiftGame.PlayerCore.PlayerCostume;
using LiftGame.PlayerCore.PlayerPowerSystem;
using LiftGame.Ui;
using UnityEngine;
using Zenject;

namespace LiftGame.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PlayerServiceProvider playerPrefab;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private InputDataProvider inputDataProvider;
        [SerializeField] private InteractionMenu interactionMenuOnCanvas;

        public override void InstallBindings()
        {
            BindPlayerData();
            BindPlayerServices();
            BindInventoryItemInteractor();
            BindInteractionMenu();
            BindPlayer();
        }

        private void BindPlayerServices()
        {
            Container.Bind<IPlayerHealthService>().To<PlayerHealthService>().FromNew().AsSingle();
            Container.Bind<IPlayerMentalService>().To<PlayerMentalService>().FromNew().AsSingle();
            Container.Bind<IPlayerPowerService>().To<PlayerPowerService>().FromNew().AsSingle();
            Container.Bind<IPlayerInputService>().To<PlayerInputService>().FromNew().AsSingle();
            Container.Bind<IPlayerAirService>().To<PlayerAirService>().FromNew().AsSingle().NonLazy();
            Container.Bind<IPlayerInventoryService>().To<PlayerInventoryService>().FromNew().AsSingle();
        }

        private void BindInventoryItemInteractor()
        {
            Container.Bind<InventoryItemInteractor>().To<InventoryItemInteractor>().FromNew().AsSingle();
        }

        private void BindInteractionMenu()
        {
            Container.Bind<InteractionMenu>().FromInstance(interactionMenuOnCanvas).Lazy();
        }
        
        private void BindPlayer()
        {
            PlayerServiceProvider player =
                Container.InstantiatePrefabForComponent<PlayerServiceProvider>(playerPrefab, spawnPoint.position, Quaternion.identity,
                    null);
            Container.Bind<PlayerServiceProvider>().FromInstance(player).AsSingle();
            Container.Bind<IPlayerCostumeService>().To<PlayerCostumeService>().FromInstance(player.PlayerCostumeService).AsSingle();
        }
        private void BindPlayerData()
        {
            playerData.ResetData();//костыль пока нет сохранений
            Container.Bind<IPlayerData>().FromInstance(playerData).AsSingle();
            Container.Bind<InputDataProvider>().FromInstance(inputDataProvider).AsSingle().Lazy();
        }
    }
}