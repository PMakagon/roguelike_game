using LiftGame.GameCore.Input;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerCostume;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PlayerServiceProvider playerPrefab;
        [SerializeField] private PlayerData playerData;

        public override void InstallBindings()
        {
            BindPlayerData();
            BindPlayerServices();
            BindPlayer();
        }

        private void BindPlayerServices()
        {
            Container.Bind<IPlayerHealthService>().To<PlayerHealthService>().FromNew().AsSingle();
            Container.Bind<IPlayerMentalService>().To<PlayerMentalService>().FromNew().AsSingle();
            Container.Bind<IPlayerPowerService>().To<PlayerPowerService>().FromNew().AsSingle();
            Container.Bind<IPlayerInputService>().To<PlayerInputService>().FromNew().AsSingle();
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
        }
    }
}