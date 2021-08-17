using Zenject;

namespace Services.SequenceProcessor.Editor.Tests
{
    public class TestInstaller : Installer
    {
        override public void InstallBindings()
        {
            Container.
                BindInterfacesAndSelfTo<ActionTestStep>().
                AsSingle();
        }
    }
}