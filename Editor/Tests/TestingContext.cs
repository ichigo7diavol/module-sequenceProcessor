using Zenject;

namespace Services.SequenceProcessor.Editor.Tests
{
    public class TestingContext
    {
        public DiContainer Container { get; private set; }

        public TestingContext()
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