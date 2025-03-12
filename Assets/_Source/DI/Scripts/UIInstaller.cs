using MarketTestCase;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MobileInput>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}