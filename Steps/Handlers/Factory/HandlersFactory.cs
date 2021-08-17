using System;
using Services.SequenceProcessor.Steps.Providers;
using Services.SequenceProcessor.Utils;

namespace Services.SequenceProcessor.Steps.Handlers.Factory
{
    public class HandlersFactory : IHandlersFactory 
    {
        private readonly IContextProvider _contextProvider;
            
        public HandlersFactory(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider ?? 
                throw new ArgumentNullException(nameof(contextProvider));
        }

        public IStepHandler Create(ISequenceStep step)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }
            IStepHandler handler;
            
            if (step.TryGetContextualStepDefinition(out var contextualType))
            {
                handler = CreateContextualStepHandler(step, contextualType);
            } 
            else if (step.TryGetParametrizedStepDefinition(out var parametrizedType))
            {
                handler = CreateParametrizedStepHandler(step, parametrizedType);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Can't Add Unsupported Step: {step.GetType()}");
            }
            return handler;
        }
        
        private IStepHandler CreateContextualStepHandler(ISequenceStep step, 
            Type interfaceDefinition)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }
            if (interfaceDefinition == null)
            {
                throw new ArgumentNullException(nameof(interfaceDefinition));
            }
            var dtoType = interfaceDefinition.
                GetContextualStepDtoType();
            
            var contextType = interfaceDefinition.
                GetContextualStepContextType();
            
            var stepHandlerCtor = interfaceDefinition.
                GetContextualStepHandlerCtorInfo(contextType, dtoType);
            
            var stepHandler = (IStepHandler) stepHandlerCtor.
                Invoke(new object[] {step, _contextProvider});

            return stepHandler;
        }
        
        private IStepHandler CreateParametrizedStepHandler(ISequenceStep step, 
            Type interfaceDefinition)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }
            if (interfaceDefinition == null)
            {
                throw new ArgumentNullException(nameof(interfaceDefinition));
            }
            var dtoType = interfaceDefinition.
                GetParametrizedStepDtoType();
            
            var stepHandlerCtor = interfaceDefinition.
                GetParametrizedStepHandlerCtorInfo(dtoType);
            
            var stepHandler = (IStepHandler) stepHandlerCtor.
                Invoke(new object[] {step});
            
            return stepHandler;
        }
    }
}