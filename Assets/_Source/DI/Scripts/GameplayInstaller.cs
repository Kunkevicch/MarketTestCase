using MarketTestCase;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PickEventMediator>()
            .AsSingle()
            .NonLazy();
    }
}