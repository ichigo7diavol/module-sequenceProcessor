using System;
using Services.SequenceProcessor.Contexts;
using Services.SequenceProcessor.Providers;
using Services.SequenceProcessor.Steps.BasicSteps;
using Services.SequenceProcessor.Steps.Handlers.Factory;
using Services.SequenceProcessor.Steps.Providers;
using Zenject;

namespace Services.SequenceProcessor
{
    public class SequenceProcessingModuleInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.
                BindInterfacesAndSelfTo<SequenceProcessingService>().
                AsSingle();
            
            Container.
                BindInterfacesAndSelfTo<SequenceStepProvider>().
                AsSingle();

            Container.
                BindInterfacesAndSelfTo<ContextProvider>().
                AsSingle();

            Container.
                BindInterfacesAndSelfTo<HandlersFactory>().
                AsSingle();
            
            InstallBaseSteps(Container);
            InstallBaseContexts(Container);
        }

        private void InstallBaseSteps(DiContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            container.
                BindInterfacesAndSelfTo<ListStep>().
                AsSingle();
        }
        
        private void InstallBaseContexts(DiContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            container.
                BindInterfacesAndSelfTo<DefaultContext>().
                AsSingle();
        }

    }
}