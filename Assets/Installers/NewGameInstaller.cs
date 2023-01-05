using LiftGame.GameCore.LevelGameLoop;
using LiftGame.GameCore.Pause;
using Zenject;

namespace LiftGame.Installers
{
    public class NewGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLevelEventHandler();
            BindPauseHandler();
        }

        private void BindLevelEventHandler()
        {
            Container.Bind<ILevelGameLoopEventHandler>().To<LevelGameLoopEventHandler>().FromNew().AsSingle();
        }
        private void BindPauseHandler()
        {
            Container.Bind<IPauseHandler>().To<PauseHandler>().FromNew().AsSingle();
        }
    }
}