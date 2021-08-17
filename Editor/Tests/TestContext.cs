using Zenject;

namespace Services.SequenceProcessor.Editor.Tests
{
    public class TestContext
    {
        public DiContainer Container { get; private set; }

        public TestContext()
        {
            Container = new DiContainer();
        }

        public void Initialize()
        {
            SetupBindings();
        
            StartResolving();
        }

        private void SetupBindings()
        {
            Container.Install<SequenceProcessingModuleInstaller>();
            Container.Install<TestInstaller>();
        }

        private void StartResolving()
        {
            Container.ResolveRoots();
        }
    }
}