using MarketTestCase;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}