using Zenject;
using GleamGames.Balls2048.Managers;
using GleamGames.Balls2048.Controllers;

namespace GleamGames.Balls2048.Installers
{
    public class DefaultInstaller : MonoInstaller<DefaultInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BallPrefabController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BallSpawnController>().FromComponentInHierarchy().AsSingle();
        }
    }
}
